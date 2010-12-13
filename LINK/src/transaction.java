

public class Transaction
{
	int transaction_id;
	User c;
	long prevReqTime; 
	
	int verifiersCount;
	int claim_status;    //Status of claim 
	Transaction(int transaction_id){
		this.transaction_id = transaction_id;
	}
}