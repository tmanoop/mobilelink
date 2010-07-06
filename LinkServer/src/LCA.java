import java.util.ArrayList;

public class LCA {
	
	int reject = -1;
	int accept = 1;
	int ignore = 0;
	ArrayList<User> mobileUsers;
	
	public LCA(){
		
	}
	public LCA(ArrayList<User> mobUsers){
		mobileUsers = mobUsers;
	}
	
	public int decisionProcess(User c){
		//below is the list of verifiers for the claimer c.
		ArrayList<User> V = new ArrayList<User>();
		
		//check the spatio-tempo corelation
		
		//check has nieghbors
		
		//check if the V is empty
		if(!V.isEmpty()){
			//call check collusion
			if(checkCollusion(V, c)){
				return reject;
			}
			//
		}
		return ignore;
		
	}
	
	public boolean checkCollusion(ArrayList<User> V, User c){
		return false;
		
	}

}
