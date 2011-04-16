import java.util.ArrayList;
import java.util.HashMap;

public class LCA {
	private static final double INC = 0.1;
	private static final double DEC = 0.5;
	private static final double INDtr = 0.3;
	private static final double AVGtr = 0.1;
	
	int reject = -1;
	int accept = 1;
	int ignore = 0;

	int POOR = 0; 
	int GOOD = 1;
	
	int tr_id = 0;
	HashMap<Integer,Transaction> transactions;
	ArrayList<User> mobileUsers;
	int[][] M;

	ArrayList<String> verifiersAddresses;
	
	// default constructor 
	public LCA() {
		
	}

	public LCA(int size) {
		transactions = new HashMap<Integer,Transaction>();
		mobileUsers = new ArrayList<User>(size);
		M = new int[size][size];
		for(int i=0;i<M.length;i++){
			User u = new User();
			u.trustScore = 0.5;
			u.serviceID = 999;
			u.id = i;
			mobileUsers.add(u);
			for(int j=0;j<M.length;j++){
				M[i][j]=0;
			}
		}
		loadVerifiersList();
	}

	private void loadVerifiersList() {
		verifiersAddresses = new ArrayList<String>();
		verifiersAddresses.add("0012D14C0895");
		verifiersAddresses.add("0012D15E190D");
		verifiersAddresses.add("0012D169146F");
		verifiersAddresses.add("00123775CDE2");
		verifiersAddresses.add("0012D1523438");
		verifiersAddresses.add("0012D15E1956");
		verifiersAddresses.add("0017E4CB0D00");
		verifiersAddresses.add("0012D17650E3");		
	}

	public boolean isValidVerifier(String address){
		if(verifiersAddresses.contains(address))
			return true;
		else
			return false;
	}
	
	synchronized public int decisionProcess(User c) {
		// below is the list of verifiers for the claimer c.
		// Get the list of verifiers available for this claimer c and also for
		// this claim ID.
		ArrayList<User> V = c.V;

		// check the spatio-tempo corelation
		if (!SpatioTempCorrelation(c)) {
			return reject(c);
		}

		// check has nieghbors
		if (hasNeighbors(c)) {
			// Get number of verifiers
			for (int i = 0; i < V.size(); i++) {
				// check the spatio-tempo corelation
				User vrfr = V.get(i);
				if (!SpatioTempCorrelation(vrfr)) {
					User usr = mobileUsers.get(vrfr.id);
					usr.trustScore = usr.trustScore * DEC;
					usr.rjctCount++;
					V.remove(vrfr);
				}
				
				double Wvi = getUpdatedWeightScore(vrfr, c);
				
				// Individual threshold to eliminate the low-scored verifiers
				if (Wvi <= INDtr) {
					V.remove(vrfr);
				}
			}
			// check if the V is empty
			if (!V.isEmpty()) {
				// call check collusion
				if (checkCollusion(V, c) != 1) {
					return reject;
				}
				
				Averages avgs = getTrustScoreAvg(V,c);
				// read and assign values to variables
				// Ysum: number of verifiers that agrees with claimer
				// Nsum: number of verifiers that disagrees with claimer
				double absAVGDiff = avgs.absAVGDiff;
				double Nsum = avgs.Nsum;
				double Ysum = avgs.Ysum;
				
				if (absAVGDiff >= AVGtr) {
					if (Ysum >= Nsum) {
						return accept(c);
					} else {
						return reject(c);
					}
				} else {
					if(trend(c) == POOR){
						return reject(c);
					} else if(verifyScoreTrends(V, c) == POOR){
						if(mobileUsers.get(c.id).trustScore <= INDtr){
							return ignore;
						} else {
							User usr = mobileUsers.get(c.id);
							usr.trustScore = usr.trustScore - INC;
							usr.rjctCount++;
							return accept;
						}
					}
				}
			}
		}

		if(trend(c) == 0){
			return reject(c);
		} else if(mobileUsers.get(c.id).trustScore <= INDtr){
			return ignore;
		}else{
			User usr = mobileUsers.get(c.id);
			usr.trustScore = usr.trustScore - INC;
			usr.rjctCount++;
			return accept;
		}
	}

	public int verifyScoreTrends(ArrayList<User> v, User c) {
		float good_cnt = 0;
		for(int i=0;i<v.size();i++) {
			int tr = trend(v.get(i));
			if(tr == 1 || tr == 2) {
				good_cnt++;
			} else if(tr == 0)  {
				mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore * DEC;
				if(mobileUsers.get(c.id).trustScore < 0)
					mobileUsers.get(c.id).trustScore = 0;
				mobileUsers.get(c.id).rjctCount = mobileUsers.get(c.id).rjctCount + 1;
			}
		} 
		float half = v.size()/2;

		if(good_cnt >= half)
			return 1;
		else
			return 0;
	}

	public boolean SecLevClaim(User C) {
		return true;
		/*
		 * if(VRFcnt == 2) { return ignore; //2nd level claim is ignored } else
		 * if(trend(c) == POOR) { Tc = Tc * Dec; usr1.trustScore =
		 * usr1.trustScore * Dec; return (reject); } else if(Tc <= INDtr) {
		 * return ignore; } else { Tc = Tc - INC; usr1.trustScore =
		 * usr1.trustScore - INC; return accept; }
		 */
	}

	public boolean hasNeighbors(User c) {
		if (c.V.isEmpty())
			return false;
		else
			return true;
	}

	public boolean SpatioTempCorrelation(User c) {

		 boolean sptCorr = true;
		 int Pre_Loc = mobileUsers.get(c.id).location; //get previous locaton of this calimer
		 //how to get x1 and x2 
		 long time1 = mobileUsers.get(c.id).time;
		 //System.out.println("time for previous location = " + time1);
		  
		 int Curr_Loc = c.location; 
		 long time2 = c.time;
		 //System.out.println("time for current location = " + time2);
		  
		 if(Pre_Loc != 0){
			 //still working on mapping the building distances 
			 int Diff_Dist = (Curr_Loc - Pre_Loc)/100; 
			 long Diff_Time = (time2-time1);
			 int diff_hrs = (int)((Diff_Time/3600) % 24);;
			 int Avg_Speed = 2; 
			 int Avg_Dist = (Avg_Speed * diff_hrs);
			  
			 if(Diff_Dist > Avg_Dist) 
				 sptCorr = false;  
			 else
				 sptCorr = true;
		 }else{
			 sptCorr = true;
		 }
		 setCurrentLocation(c);
		 //temporarily always return true
		 return true;
	}

	// to find the trustscore of the user
	// public double getTrustScore(ArrayList<User> V, User c) {
	// return POOR;
	// }

	public Averages getTrustScoreAvg(ArrayList<User> V, User c) {

		double Y=0;
		double N=0;
		double indThrld = 0.3;
		Averages avg = new Averages();
		
		for (int i = 0; i < V.size(); i++) {
			User vrfr = V.get(i);
			double Wvi = getUpdatedWeightScore(vrfr,c);
			//finally update verifier Matrix once in each claim for each verifier
			M[vrfr.id][c.id]++;
			if(mobileUsers.get(vrfr.id).trustScore > indThrld){
				if (c.location == vrfr.location) { 
					Y = Y + Wvi; 
				}else { 
					N = N + Wvi;  
				} 
			}
		}
		if(Y > N)
			avg.absAVGDiff = (Y - N)/V.size();
		else
			avg.absAVGDiff = (N - Y)/V.size();
		avg.Ysum = Y;
		avg.Nsum = N;
		return avg;

	}

	// to calculate the updated score of the user
	public double getUpdatedWeightScore(User v, User c) {
		double vWeightedScore = 0.0;
		int w = M[v.id][c.id];
		// vWeightedScore = Tv/log2(w);
		if(w>1)
			vWeightedScore = mobileUsers.get(v.id).trustScore / log2(w);
		else
			vWeightedScore = mobileUsers.get(v.id).trustScore;
		
		return vWeightedScore;
	}

	// to find the trend score of the user
	public int trend(User c) {
		// get below details of from mobileUsers arraylist
		int totClc = mobileUsers.get(c.id).totalCount;
		int rejClc = mobileUsers.get(c.id).rjctCount;
		if(totClc >= 10){
			double perc = (double)rejClc/totClc;
			perc = perc*100;
			if(perc >= 10) 
				return 0; // reject
			else
				return 1;
		} else 
			return 2; //ignore
	}

	public int checkCollusion(ArrayList<User> V, User c) {
		double B = 0.3;
		// Wmax set to B i.e. 30%
		int Wmax = (int) (B * mobileUsers.get(c.id).totalCount);
		int Wrst = 1;
		int k = 0;
		int m = 0;
		double A = 0.1;
		// Total number of claims made by a claimer
		
		
		if(mobileUsers.get(c.id).totalCount < 10) 
			return 1;
		for (int i = 0; i < M.length; i++) { 
			if (M[i][c.id] >= Wmax)
				k++;
			if(M[i][c.id] > 0)
				m++;
		}
		// A set to 10%
		if (m!=0 && (k / m) > A) {
			boolean pun = false;
			for (int i = 0; i > M.length; i++) 
			{
				if ((M[i][c.id] >= Wmax)){
					// for loop for V.arraylist :-
					for(int z=0; z<c.V.size(); z++){
						if(i == c.V.get(z).id)
							pun = true;
					}
					if(mobileUsers.get(i).punished == false){
						pun = true;
					}
					if(pun == true){
						mobileUsers.get(i).trustScore = mobileUsers.get(i).trustScore * DEC;
						mobileUsers.get(i).rjctCount++;
						mobileUsers.get(i).punished = true;
					}
				}
			}
			return 0; // bad
		}

		else {
			for (int i = 0; i > V.size(); i++) {
				if (M[V.get(i).id][c.id] >= Wmax)
					M[V.get(i).id][c.id] = Wrst; // Wrst is reset value
			}
			return 1; // good
		}
	} 

	public double log2(double num) {
		return (Math.log(num) / Math.log(2));
	}
	
	public void setCurrentLocation(User c){
		//this latest location used later to verify the spatio-temp correlation
		mobileUsers.get(c.id).location = c.location;
		mobileUsers.get(c.id).time = c.time;
	}
	
	public int reject(User c){
		User usr = mobileUsers.get(c.id);
		usr.trustScore = usr.trustScore * DEC;
		usr.rjctCount++;
		
		return reject;
	}
	
	public int accept(User c){
		User usr = mobileUsers.get(c.id);
		usr.trustScore = usr.trustScore + INC;
		
		return accept;
	}
} // class closes

