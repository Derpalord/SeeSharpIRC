using System;
using System.Text.RegularExpressions;

public class Message
{
    public class Tag
    {
        class Key
        {
            static private readonly Regex rx = new Regex(@"^[a-zA-Z0-9\-]+$", RegexOptions.Compiled);
            public string ClientPrefix {get;}
            public string Vendor {get;}
            public string Keyname {get;}
            public Key(string rawKey) {
                int index = 0;
                if (rawKey[index] == '+') {
                    ClientPrefix = "+";
                    index += 1;
                }
                int slashIndex = rawKey.IndexOf('/');
                if (slashIndex != -1) {
                    Vendor = rawKey.Substring(index, slashIndex-index);
                    UriHostNameType vType = Uri.CheckHostName(Vendor);
                    if (vType != UriHostNameType.Dns) {
                        throw new Exception("Failed key parsing: Invalid vendor hostname");
                    }
                    index = slashIndex+1;
                }
                Match keynameMatch = rx.Match(rawKey, slashIndex);
                if (!keynameMatch.Success) {
                    throw new Exception("Failed key parsing: Invalid key name");
                }
                Keyname = keynameMatch.Value;
            }
        }
        class Value
        {
            static private readonly Regex rx = new Regex(@"^[^\x00\r\n;\ ]*$", RegexOptions.Compiled);
            public string Seq {get;}
            public Value(string rawValue) {
                if (!rx.IsMatch(rawValue)) {
                    throw new Exception("Failed value parsing.");
                }
                Seq = rawValue;
            }
        }
    }
    public Tag VTag {
        get; set;
    }
}