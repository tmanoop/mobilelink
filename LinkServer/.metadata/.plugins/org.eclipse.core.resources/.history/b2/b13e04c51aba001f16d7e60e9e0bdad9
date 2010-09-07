import java.util.ArrayList;
import java.io.*;

public class LCA
{
	private static final double Dec = 0.5;
	int reject = -1;
	int accept = 1;
	int ignore = 0;
	int VRFcnt = 0 ;
	double Tc = 0;
	double Tvi = 0;
	
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
	    if(SpatioTempCorrelation(c))
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
			    if(SpatioTempCorrelation(V))
			    {
			    	 double Tvi = Tvi * Dec;
			    	 temp = V[i];
			    	 return reject;
			      }
			    Wvi = getUpdatedWeightScore(V[i],c);
			    if(Wvi <= INDtr)
		    }
		  return (accept); 
	    }
	  
				
		//check if the V is empty
		if(!V.isEmpty())
		{
			//call check collusion
			if(checkCollusion(User V, c))
			{
				return (reject);
			}
		}
		
			
	private void (private boolean hasNeighbors(User c)) 
	{
		// TODO Auto-generated method stub
		return (false);
	}
	
	public boolean SpatioTempCorrelation(User c) 
	{
		        string Pre_Loc= "x1"         //get previous locaton of this calimer ?how to get  x1 and x2
			    Date date1 = new Date();
			    System.out.println(date1);
			    long time1 = date.getTime();
			    System.out.println("time for previous location = " + time1);

			    string Curr_Loc= "X2";
			    Date date2 = new Date();
			    System.out.println(date2);
			    long time2 = date.getTime();
			    System.out.println("time for current location = " + time22);

			    int Diff_Dist = (x2-x1);
			    Diff_Time=(time2-time1);

			    Avg_Speed="20 km/hr";
			    Avg_Dist= (Avg_Speed * Diff_Time);
			     
			    if(Diff_Dist >Avg_Dist)
			         return(false);
			    else
			         return(true);
	   }
		// TODO Auto-generated method stub
		return (false);
	}
	
	
	
	public boolean LCA.checkCollusion(ArrayList<User> verifier V, User c)
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
