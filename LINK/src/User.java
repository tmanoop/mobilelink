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
	public String getMOBIP() {
		return MOBIP;
	}
	public void setMOBIP(String mOBIP) {
		if(mOBIP.contains("/"))
			mOBIP = mOBIP.substring(1, mOBIP.length());
		MOBIP = mOBIP;
	}
	User() {
		V = new ArrayList<User>();
	}
}
