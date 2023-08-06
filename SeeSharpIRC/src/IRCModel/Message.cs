using System;

public class Message
{
    public class Tag
    {
        class Key
        {
            string client_prefix;
            string vendor;
            string keyname;
            public Key(string raw_key) {
                if (raw_key[0] == '+') {
                    client_prefix = "+";
                }
                
            }
        }
        class Value
        {

        }
    }
    public Tag VTag {
        get; set;
    }
}