import java.util.ArrayList;

public class User {
	int id;
	int location;
	int serviceID;
	long time;
	double trustScore;

	int totalCount;
	int rjctCount;
	ArrayList<User> V;

	boolean punished;
	
	String MOBIP;
	User() {
		V = new ArrayList<User>();
	}
}
