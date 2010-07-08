import java.util.ArrayList;
import java.io.*;

public class LCA
{
	private static final double Dec = 0.5;
	int reject = -1;
	int accept = 1;
	int ignore = 0;
	int VRFcnt = 0 ;
	int POOR = 0;     //TODO to be assigned a proper value
	double Tc = 0;
	double Tvi = 0;
	double Inc = 0.1;
	boolean secVer;
	
	
	ArrayList<User> mobileUsers;
	
	public LCA()
	{
		
	}
	public LCA(ArrayList<User> mobUsers)
	{
		mobileUsers = mobUsers;
	}
	
	public int decisionProcess(User c)
	{
		//below is the list of verifiers for the claimer c.
		ArrayList<User> V = new ArrayList<User>();
		
		//check the spatio-tempo corelation
	    if(!SpatioTempCorrelation(c))
	    {
		return reject;
		}
	    double Tc  = Tc * Dec;
	    VRFcnt++;
		
		//check has nieghbors
	    if(hasNeighbors(User c))
	    {
		   for(int i = 0; i <= n; i++)
		   {
			//check the spatio-tempo corelation
			    if(!SpatioTempCorrelation(V[i]))
			    {
			    	 double Tvi = Tvi * Dec;
			    	 temp = V[i];
			    	 return reject;
			      }
			    Wvi = getUpdatedWeightScore(V[i],c);
			    // Individual threshold to eliminate the low-scored verifiers
			    if(W.V[i] <= INDtr)                       
			    {
			    	temp1 = V[i]; 
			    }
			    
		    }
		  return accept; 
	    }
	  
				
		//check if the V is empty
		if(!V.isEmpty())
		{
			//call check collusion
			if(checkCollusion(User V, c))
			{
				return (reject);
			}
			//Ysum: number of verifiers that agrees with claimer
			//Nsum: number of verifiers that disagrees with claimer
			(absAVGDiff, Ysum, Nsum) = getTrustScore(V,c);
			if(absAVGDiff >= AVGtr)
			{
				if(Ysum >= Nsum)
				{
					Tc = Tc +  INC;
					return accept;
				}
				else
				{
					Tc = Tc + Dec;
					return reject;
				}
			}
			else
			{
				if(trend(c) == POOR)
				{
					Tc = Tc * Dec;
					return reject;
				}
				else if(verifyScoreTrends(V[n] == Poor ))
				{
					if(Tc <= INDtr)
						return ignore;
					else
						Tc = Tc -  INC;
					    return accept;
				}
				else if(VRFcnt == 2)
				{
					return ignore;               //2nd level claim is ignored
				}
				else 
				{
					for(i = 0;i <= V[n].size())                //TODO V[n]to be defined
					{
						SecVer[i] = decisionProcess(Vi,Lvi);       //Recursive call
					}
					if(Majority(SecVer) == ignore || reject)
					{
						Tc = Tc +  INC;
						return accept;
					}
					else
					{					
						Tc = Tc * Dec;
						return reject; 
					}
					
				 }
				
				
			}
			
			
		}
		
   public boolean SecLevClaim(C)
   {
		if(VRFcnt == 2)
		{
			return ignore;                                          //2nd level claim is ignored
		}
		else if(trend(c) == POOR)
		 {
			Tc = Tc * Dec;	
			return (reject);
		 }
		else if(Tc <= INDtr)
		{
			return ignore;
		}
		else 
		{
			Tc = Tc - INC;	
			return accept;
		}
   }	
			
	public boolean hasNeighbors(User c) 
	{
		// TODO Auto-generated method stub
		return (false);
	}
	
	public boolean SpatioTempCorrelation(User c) 
	{
		        string x1,x2;     
		        string Pre_Loc = "x1"         //get previous locaton of this calimer ?how to get  x1 and x2
			    Date date1 = new Date();
			    System.out.println(date1);
			    long time1 = date.getTime();
			    System.out.println("time for previous location = " + time1);

			    string Curr_Loc = "X2";
			    Date date2 = new Date();
			    System.out.println(date2);
			    long time2 = date.getTime();
			    System.out.println("time for current location = " + time22);

			    int Diff_Dist = (x2-x1);
			    Diff_Time=(time2-time1);

			    Avg_Speed="20 km/hr";
			    Avg_Dist= (Avg_Speed * Diff_Time);
			     
			    if(Diff_Dist > Avg_Dist)
			         return(false);
			    else
			         return(true);
	   }
		// TODO Auto-generated method stub
		return (false);
	}
	
	//to find the trustscore of the user
	public double getTrustScore(V,c)
	{
	}
		public double getTrustScoreAvg(V,c)
		{
		 
		  String Ltr;
		  long Y,Yavg,ycnt;
		  long N,Navg,ncnt;
		 
		     try
		       {
		   	  for(i = 0;i=V.size();i++)
			     {
		 		Wvi = getUpdatedWeightScore(vi,c);
				     if ((LC ?? Lvi) || Ltr)
					{
		   			  Y = Y + Wvi; 
		   			  ycnt++;
					}
		  		     else
					 {
		  			   VN.add(vi)
		   		           N = N + Wvi; ncnt++;
					 }
		 		    end if
		  		    end for(absAVGDiff = (Y * ycnt - N * ncnt)/V.size())
		   			{
					  Yavg = Y/ycnt;
				          Navg = N/ncnt;
		   			  return(absAVGDiff, Yavg, Navg);
					 }
		    	    }
			}
		      
		}
		
	}
	
// to calculate the updated score of the user
	public getUpdatedWeightScore(v,c):
	{
	     w = M[v][c];
	     Vwscore = Tv/log2(w);
	     M[v][c]++;
	     return(Vwscore);
	   }
	}
	
	// to find the trend score of the user
	public trend(c)
	{
      	 trendScores=(RejClc/NumClc);
	}
		
	
	public boolean checkCollusion(ArrayList<User> verifier V, User c)
	{ 
		 int m = 0;
	     int k = 0;
	     double B = 0.3; 
	     int NumClc = 0;
	     int Wrst;
	   //Wmax set to B i.e. 30%
	     double A = 0.1;          
	   //Total number of claims made by a claimer
	     double Wmax = B * NumClc;
	     for( int i = 0; i = M.size; i++)
	     {              // TODO where to define matrix?
	       if(M[i][c] >= Wmax)
	 	       k++;
	       if(M[i][c])
	       {
	    	   return reject;
	           m++;
	 	     }
	     }
	 	   //A set to 10%
	     if(k/m >= A)
	     {                   
	       	Tc = Tc * Dec;
	 	    for(i = 0 ;i = M:size;i++)
	 	    {
	 	      if ((M[i][c] >= Wmax) && ((i 2 V ) ||(i:punished == false)))
	 	      {
			    double Ti = Ti * DEC;
	 		    boolean i:punished = true;
	 	       }
	 	     }
	 	  return(ignore);
	     }
	       
	    else
	    {
	        for( int i = 0; i = V:size; i++)
	        {
		     if( M[V [i]][c] >= Wmax)    	 
			     M[V[i]][c] = Wrst;                  //Wrst is reset value
	        }
	 	    return ignore;
	       }
	   }
	   
	return ignore;	
	}

}
