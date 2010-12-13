import java.io.IOException;
import java.math.BigInteger;
import java.security.Key;
import java.security.KeyFactory;
import java.security.KeyPair;
import java.security.KeyPairGenerator;
import java.security.NoSuchAlgorithmException;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.SecureRandom;
import java.security.Signature;
import java.security.interfaces.RSAPrivateKey;
import java.security.interfaces.RSAPublicKey;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.KeySpec;
import java.security.spec.RSAPrivateKeySpec;
import java.security.spec.RSAPublicKeySpec;
import java.util.ArrayList;

import javax.crypto.Cipher;
import javax.crypto.SealedObject;

import org.apache.commons.codec.binary.Base64;

import sun.misc.BASE64Decoder;


public class DigitalSignature {
	static private final int BITS_IN_MODULUS = 1024;
	protected static KeyFactory fact;
	public DigitalSignature() {
		// TODO Auto-generated constructor stub
		try {
			fact = KeyFactory.getInstance("RSA");
		} catch (NoSuchAlgorithmException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	/**
	 * @param args
	 */
	public static void main1(String[] args) {

		try {
			fact = KeyFactory.getInstance("RSA");
			// Get an instance of the RSA key generator
			KeyPairGenerator kpg = KeyPairGenerator.getInstance("RSA");
			// Generate the keys — might take sometime on slow computers
			KeyPair myPair = kpg.generateKeyPair();
			String originalData = "Test";
			//SealedObject signedData = HashAndSignBytes(originalData,myPair);
			byte[] signedData = encryptAndSign(originalData);
			System.out.println(signedData.length);
			//if(VerifySignedHash(originalData, signedData, myPair)){
			if(decryptAndVerify(originalData, signedData)){
				System.out.println("Test Success!!");
			}else{
				System.out.println("Test Failed!!");
			}
			
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static SealedObject HashAndSignBytes(String DataToSign, KeyPair myPair)
    {
        try
        {
        	// Get an instance of the Cipher for RSA encryption/decryption
        	Cipher c = Cipher.getInstance("RSA");
        	// Initiate the Cipher, telling it that it is going to Encrypt, giving it the public key
        	c.init(Cipher.ENCRYPT_MODE, myPair.getPublic());
        	
        	// Create a secret message
        	String myMessage = new String(DataToSign);
        	// Encrypt that message using a new SealedObject and the Cipher we created before
        	SealedObject myEncyptedMessage = new SealedObject( myMessage, c);
        	return myEncyptedMessage;
        }
        catch (Exception e)
        {
            e.printStackTrace();
            return null;
        }
    }

    public static boolean VerifySignedHash(String DataToVerify, SealedObject SignedData, KeyPair myPair)
    {
        try
        {// Get an instance of the Cipher for RSA encryption/decryption
        	Cipher dec = Cipher.getInstance("RSA");
        	// Initiate the Cipher, telling it that it is going to Decrypt, giving it the private key
        	dec.init(Cipher.DECRYPT_MODE, myPair.getPrivate());

        	// Tell the SealedObject we created before to decrypt the data and return it
        	String message = (String)SignedData.getObject(dec);
        	if(DataToVerify.equals(message))
        		return true;
        	else
        		return false;
        }
        catch (Exception e)
        {
           e.printStackTrace();
            return false;
        }
    }
    
    public static byte[] encryptAndSign(String dataToEncrypt){
    	   byte[] encryptedString = null;
		try {
			byte[] modulusBytes = Base64
					.decodeBase64("xTSiS4+I/x9awUXcF66Ffw7tracsQfGCn6g6k/hGkLquHYMFTCYk4mOB5NwLwqczwvl8HkQfDShGcvrm47XHKUzA8iadWdA5n4toBECzRxiCWCHm1KEg59LUD3fxTG5ogGiNxDj9wSguCIzFdUxBYq5ot2J4iLgGu0qShml5vwk=");
			byte[] exponentBytes = Base64.decodeBase64("AQAB");
			BigInteger modulus = new BigInteger(1, modulusBytes);
			BigInteger exponent = new BigInteger(1, exponentBytes);
			RSAPublicKeySpec rsaPubKey = new RSAPublicKeySpec(modulus, exponent);
			//KeyFactory fact = KeyFactory.getInstance("RSA");
			PublicKey pubKey = fact.generatePublic(rsaPubKey);
			Cipher cipher = Cipher.getInstance("RSA");
			cipher.init(Cipher.ENCRYPT_MODE, pubKey);
			if(dataToEncrypt==null)
				dataToEncrypt = "big kitty dancing";
			byte[] plainBytes = new String(dataToEncrypt)
					.getBytes("UTF-8");
			byte[] cipherData = cipher.doFinal(plainBytes);
			encryptedString = Base64.encodeBase64(cipherData);
		} catch (Exception e) {
			e.printStackTrace();
			// TODO: handle exception
		}
    	   return encryptedString;
    }
    
    public static boolean decryptAndVerify(String originalData, byte[] encryptedString){
    	byte[] decryptedData = null;
		try {
			byte[] modulusBytes = Base64
					.decodeBase64("xTSiS4+I/x9awUXcF66Ffw7tracsQfGCn6g6k/hGkLquHYMFTCYk4mOB5NwLwqczwvl8HkQfDShGcvrm47XHKUzA8iadWdA5n4toBECzRxiCWCHm1KEg59LUD3fxTG5ogGiNxDj9wSguCIzFdUxBYq5ot2J4iLgGu0qShml5vwk=");
			byte[] exponentBytes = Base64.decodeBase64("AQAB");
			BigInteger modulus = new BigInteger(1, modulusBytes);
			BigInteger exponent = new BigInteger(1, exponentBytes);
			RSAPrivateKeySpec rsaPrvKey = new RSAPrivateKeySpec(modulus,
					exponent);
			//KeyFactory fact = KeyFactory.getInstance("RSA");
			PrivateKey prvKey = fact.generatePrivate(rsaPrvKey);
			Cipher cipher = Cipher.getInstance("RSA");
			cipher.init(Cipher.DECRYPT_MODE, prvKey);
			decryptedData = cipher.doFinal(encryptedString);
			System.out.println("Length: "+decryptedData.length);
		} catch (Exception e) {
			e.printStackTrace();
			return false;
			// TODO: handle exception
		}
		//String decryptedString = Base64.encodeBase64(decryptedData);
		String decryptedString = new String(decryptedData);
		  if(decryptedString == originalData)
			  return true;
		  else
			  return false;
    }
    
    public static void main(String[] args) throws Exception {
    	// Generate an RSA key

    	KeyPairGenerator kpg = KeyPairGenerator.getInstance("RSA");

    	SecureRandom rng = new SecureRandom();

    	kpg.initialize(BITS_IN_MODULUS, rng);

    	KeyPair kp = kpg.generateKeyPair();

    	RSAPublicKey rsaPubkey = (RSAPublicKey) kp.getPublic();

    	RSAPrivateKey rsaPrivkey = (RSAPrivateKey) kp.getPrivate();

    	System.out.println("Algorithm: "+rsaPubkey.getAlgorithm());
    	System.out.println("Modulus: ");
    	System.out.println(rsaPubkey.getModulus());
    	System.out.println("Public Exp: ");
    	System.out.println(rsaPubkey.getPublicExponent());
    	
    	RSAPublicKey rsaPubk = (RSAPublicKey)readPublicKey(rsaPubkey.getModulus(),rsaPubkey.getPublicExponent());
    	
    	System.out.println("Modulus: ");
    	//System.out.println(rsaPubk.getModulus());
    	System.out.println("Public Exp: ");
    	//System.out.println(rsaPubk.getPublicExponent());
    	System.out.println(rsaPubk);
    	
    	String message = "Hello! World!!";
    	
    	byte [] signature = sign(message, rsaPrivkey);

    	System.out.println(new BigInteger(1, signature).toString(16));

    	// Now verify it

    	System.out.println(verify(message.getBytes(), rsaPubkey, signature));
      }
    
    public static byte[] sign(String message, RSAPrivateKey rsaPrivkey){
    	byte [] signature = null;
		try {
			byte[] tbsMessage = message.getBytes("UTF-8");
			Signature signer = Signature.getInstance("SHA1withRSA");
			signer.initSign(rsaPrivkey);
			signer.update(tbsMessage);
			signature = signer.sign();
		} catch (Exception e) {
			e.printStackTrace();
			// TODO: handle exception
		}
    	return signature;
    	
    }
    
    public static boolean verify(byte[] tbsMessage, RSAPublicKey rsaPubkey, byte[] signature){

    	Signature verifier;

    	try {
			//byte[] tbsMessage = message.getBytes("UTF-8");
			verifier = Signature.getInstance("SHA1withRSA");
			verifier.initVerify(rsaPubkey);
			verifier.update(tbsMessage);
			return verifier.verify(signature);
		} catch (Exception e) {
			e.printStackTrace();
			return false;
		}
    }
    
    public static PublicKey readPublicKey(BigInteger m, BigInteger exp){
    	try {
    		//byte[] modulusBytes = Base64.decodeBase64(m);
    		//byte[] exponentBytes = Base64.decodeBase64(exp);
			//BigInteger modulus = new BigInteger(1, modulusBytes);
			//BigInteger exponent = new BigInteger(1, exponentBytes);
			//BigInteger modulus = new BigInteger(m);
			//BigInteger exponent = new BigInteger(exp);
			RSAPublicKeySpec rsaPubKey = new RSAPublicKeySpec(m, exp);
			KeyFactory fact = KeyFactory.getInstance("RSA");
			PublicKey pubKey = fact.generatePublic(rsaPubKey);
			return pubKey;
		} catch (NoSuchAlgorithmException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		} catch (InvalidKeySpecException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		}
    }
}
