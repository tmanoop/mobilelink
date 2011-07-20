package com.example.helloandroid;

import java.math.BigInteger;
import java.security.KeyFactory;
import java.security.KeyPair;
import java.security.KeyPairGenerator;
import java.security.NoSuchAlgorithmException;
import java.security.PublicKey;
import java.security.SecureRandom;
import java.security.Signature;
import java.security.interfaces.RSAPrivateKey;
import java.security.interfaces.RSAPublicKey;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.RSAPublicKeySpec;

public class DigitalSignature {
	
	static private final int BITS_IN_MODULUS = 1024;
	
	static RSAPublicKey rsaPubkey;

	static RSAPrivateKey rsaPrivkey;
	
	public DigitalSignature(){
    	try {
			// Generate an RSA key

			KeyPairGenerator kpg = KeyPairGenerator.getInstance("RSA");

			SecureRandom rng = new SecureRandom();

			kpg.initialize(BITS_IN_MODULUS, rng);

			KeyPair kp = kpg.generateKeyPair();

			rsaPubkey = (RSAPublicKey) kp.getPublic();
			rsaPrivkey = (RSAPrivateKey) kp.getPrivate();

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
		} catch (NoSuchAlgorithmException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
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
	
	public static void main(String[] args) throws Exception {

    	
    	String message = "Hello! World!!";
    	
    	byte [] signature = sign(message, rsaPrivkey);

    	System.out.println(new BigInteger(1, signature).toString(16));

    	// Now verify it

    	System.out.println(verify(message.getBytes(), rsaPubkey, signature));
      }

}
