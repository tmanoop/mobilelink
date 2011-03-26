using System;

public class PowerStat
{
    public PowerStat()
	{
	}
    String voltage = "";
    String current = "";
    String timestamp = "";
    public String getVoltage()
    {
        return voltage;
    }
    public String getCurrent()
    {
        return current;
    }
    public String getTimestamp()
    {
        return timestamp;
    }
    public void setVoltage(String volt)
    {
        voltage = volt;
    }
    public void setCurrent(String curr)
    {
        current = curr;
    }
    public void setTimestamp(String time)
    {
        timestamp = time;
    }
}
