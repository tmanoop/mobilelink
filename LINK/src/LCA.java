import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Date;

public class LCA {
	private static final double DEC = 0.5;
	private static final double INDtr = 0.3;
	private static final int AVGtr = 0;
	private static final double INC = 0;
	int reject = -1;
	int accept = 1;
	int ignore = 0;
	int VRFcnt = 0;
	int POOR = 0; // TODO to be assigned a proper value
	int GOOD = 1;
	// double Tc = 0;
	// double Tvi = 0;
	// double Inc = 0.1;
	boolean secVer;

	ArrayList<User> mobileUsers;
	int[][] M;

	/*
	 * public LCA() //constructor {
	 * 
	 * }
	 */
	public LCA(ArrayList<User> mobUsers, int[][] Matrix) {
		mobileUsers = mobUsers;
		M = Matrix;
	}

	public int decisionProcess(User c) {
		// below is the list of verifiers for the claimer c.
		// Get the list of verifiers available for this claimer c and also for
		// this claim ID.
		ArrayList<User> V = c.V;

		// check the spatio-tempo corelation
		if (!SpatioTempCorrelation(c)) {
			User usr1 = mobileUsers.get(c.id);
			usr1.trustScore = usr1.trustScore * DEC;
			return reject;
		}

		VRFcnt++;

		// check has nieghbors
		if (!V.isEmpty()) {
			// Get number of verifiers
			for (int i = 0; i <= V.size(); i++) {
				// check the spatio-tempo corelation
				User vrfr = V.get(i);
				if (!SpatioTempCorrelation(vrfr)) {
					User usr = mobileUsers.get(vrfr.id);
					usr.trustScore = usr.trustScore * DEC;
					V.remove(vrfr);
					return reject;
				}
				double Wvi = getUpdatedWeightScore(V.get(i), c);
				// Individual threshold to eliminate the low-scored verifiers
				if (Wvi <= INDtr) {
					V.remove(vrfr);
				}
			}
			// check if the V is empty
			if (!V.isEmpty()) {
				// call check collusion
				if (checkCollusion(V, c) == 1) {
					return (reject);
				}
				// Ysum: number of verifiers that agrees with claimer
				// Nsum: number of verifiers that disagrees with claimer
				int absAVGDiff = 0;
				int Nsum = 0;
				int Ysum = 0;
				// averages = getTrustScore(V,c);
				//read and assign values to above variables

				if (absAVGDiff >= AVGtr) {

					if (Ysum >= Nsum) {
						// Tc = Tc + INC;
						mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore + INC;
						//usr1.trustScore = usr1.trustScore + INC;
						return accept;
					} else {
						// Tc = Tc + Dec;
						mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore * DEC;
						return reject;
					}
				} else {
					return accept;
					/*
					if (!trend(c)) {
						// Tc = Tc * Dec;
						mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore * DEC;
						return reject;
					} else if (!verifyScoreTrends(V)) {
						if (c.trustScore <= INDtr)
							return ignore;
						else
							// Tc = Tc - INC;
							mobileUsers.get(c.id).trustScore = mobileUsers.get(c.id).trustScore - INC;
						return accept;
					} else if (VRFcnt == 2) {
						return ignore; // 2nd level claim is ignored
					} else { //comment this else and test later
						
						int[] SecVer = null;
						for (int i = 0; i <= V.size(); i++) // TODO Vn to be
															// defined
						{
							// SecVer = null;
							// SecVer[i] = decisionProcess(Vi,Lvi); //Recursive
							// call
						}
						if ((Majority(SecVer) == ignore)
								|| (Majority(SecVer) == reject)) {
							// Tc = Tc + INC;
							//usr1.trustScore = usr1.trustScore + INC;
							return accept;
						} else {
							// Tc = Tc * Dec;
							//usr1.trustScore = usr1.trustScore * Dec;
							return reject;
						}
						
					}*/
				}
			}
		}
		
		//if(VRFcnt == 2) finish this block

		return POOR;
	}

	private int Majority(int[] secVer2) {
		// TODO Auto-generated method stub
		return 0;
	}

	private boolean verifyScoreTrends(ArrayList<User> v) {
		// TODO Auto-generated method stub
		// calling trend(c) for each verifier
		
		if (trend(v.get(0)))
			return true;
		else
			return false;
	}

	public boolean SecLevClaim(User C) {
		return secVer;
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

		/*
		 * int Pre_Loc = c.prevLocation; //get previous locaton of this calimer
		 * ?how to get x1 and x2 long time1 = c.prevTime;
		 * System.out.println("time for previous location = " + time1);
		 * 
		 * int Curr_Loc = c.location; long time2 = c.time;
		 * System.out.println("time for current location = " + time2);
		 * 
		 * int Diff_Dist = (Curr_Loc - Pre_Loc); long Diff_Time = (time2-time1);
		 * 
		 * int Avg_Speed = 20; long Avg_Dist = (Avg_Speed * Diff_Time);
		 * 
		 * if(Diff_Dist > Avg_Dist) return(false); else
		 */
		return (true);
	}

	// to find the trustscore of the user
	public double getTrustScore(ArrayList<User> V, User c) {
		return POOR;
	}

	public double getTrustScoreAvg(ArrayList<User> V, User c) {

		String Ltr;
		long Y, Yavg, ycnt;
		long N, Navg, ncnt = 0;

		for (int i = 0; i < V.size(); i++) {

			// int Wvi = getUpdatedWeightScore(V.get(i),c);
			/*
			 * if ((LC - Lvi) > Ltr) { Y = Y + Wvi; ycnt++; } else { VN.add(vi)
			 * N = N + Wvi; ncnt++; } end if end for(absAVGDiff = (Y * ycnt - N
			 * * ncnt)/V.size()) { Yavg = Y/ycnt; Navg = N/ncnt;
			 * return(absAVGDiff, Yavg, Navg); } }
			 */

		}
		return ncnt;

	}

	// to calculate the updated score of the user
	public double getUpdatedWeightScore(User v, User c) {
		double vWeightedScore = 0.0;
		int w = M[v.id][c.id];
		// vWeightedScore = Tv/log2(w);
		vWeightedScore = v.trustScore/log2(w);
		M[v.id][c.id]++;
		return vWeightedScore;
	}

	// to find the trend score of the user
	public boolean trend(User c) {
		// get below details of from mobileUsers arraylist
		int NumClc = c.totalCount;
		int RejClc = c.rjctCount;
		double trendScores = (RejClc / NumClc);
		if (trendScores > 0.1)
			return false;
		else
			return true;
	}

	public int checkCollusion(ArrayList<User> V, User c) {
		int m = 0;
		int k = 0;
		double B = 0.3;
		double RejClc = 0;
		double NumClc = 0;
		int Wrst = 0;
		int Wmax;
		// Wmax set to B i.e. 30%
		double A = 0.1;
		// Total number of claims made by a claimer
		Wmax = (int) (B * NumClc);
		for (int i = 0; i < M.length; i++) { // TODO where to define matrix?
			if (M[i][c.id] >= Wmax)
				k++;

		}
		// A set to 10%
		if (k / m >= A) {
			// Tc = Tc * Dec;
			for (int i = 0; i > M.length; i++) // matrix need to be defined
			{
				if ((M[i][c.id] >= Wmax)) // M[i][c] to be defined ||(i.punished
											// == false)
				{
					//for loop for V.arraylist :- 
						//if check whether the i == V.ID 
							// double Ti = Ti * DEC;
							// boolean i.punished = true; //i.punished???
					
					//if check i.punished == false
						// double Ti = Ti * DEC;
						// boolean i.punished = true; //i.punished???
				}
			}
			return 0; //bad
		}

		else {
			for (int i = 0; i > V.size(); i++) {
				if (M[V.get(i).id][c.id] >= Wmax)
					M[V.get(i).id][c.id] = Wrst; // Wrst is reset value
			}
			return 1; //good
		}
	} // Decision function closes
	
	public static double log2(double num)
	{
		return (Math.log(num)/Math.log(2));
	} 
} // class closes

