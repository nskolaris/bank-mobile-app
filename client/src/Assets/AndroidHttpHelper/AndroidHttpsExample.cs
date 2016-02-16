using UnityEngine;
using System.Collections;

public class AndroidHttpsExample : MonoBehaviour
{
    private bool mInitialized = false;

    private string mResponse = "";

    void Awake()
    {
		string cert1 = @"-----BEGIN CERTIFICATE-----
MIIFaTCCBFGgAwIBAgIQa1Nm3qM+kHLYOWIS4eblOjANBgkqhkiG9w0BAQsFADCB
kDELMAkGA1UEBhMCR0IxGzAZBgNVBAgTEkdyZWF0ZXIgTWFuY2hlc3RlcjEQMA4G
A1UEBxMHU2FsZm9yZDEaMBgGA1UEChMRQ09NT0RPIENBIExpbWl0ZWQxNjA0BgNV
BAMTLUNPTU9ETyBSU0EgRG9tYWluIFZhbGlkYXRpb24gU2VjdXJlIFNlcnZlciBD
QTAeFw0xNTExMDIwMDAwMDBaFw0xODExMDEyMzU5NTlaMFsxITAfBgNVBAsTGERv
bWFpbiBDb250cm9sIFZhbGlkYXRlZDEUMBIGA1UECxMLUG9zaXRpdmVTU0wxIDAe
BgNVBAMTF2FwcHMtbGFuemFsbGFtYXMuY29tLmFyMIIBIjANBgkqhkiG9w0BAQEF
AAOCAQ8AMIIBCgKCAQEA7+v+eG+i4oTaXS7cj5XmHRftHyAfK8JH+zGnPvyzLDp3
QkWinW+DylbPj/hR70MnzJno0E9W7+P1sSFpiaqmo0h9mCEQVuEbi512i2RcRivZ
1VXFsn+LVkA+mTXSe4I8mSwA8TS6yVDZM4MR5HEAms2Pugf5Iq1mXCX2VVYEsj7v
usrV4obClm4t/I+2MTJYJyhxjrGooDbnJWdnEtPxmTRxnTezQlUqxz8y06EKLif5
4Ju9nai/htqoIwKNT9wEL7gHfT6yKO7ZHaaEHeRvKjEE+m6nMCzKBjJFfmORTf+t
gTEjbV+lHteIuik1d3FrnnDT1pwQHdR5VFQu0ETsVQIDAQABo4IB8TCCAe0wHwYD
VR0jBBgwFoAUkK9qOpRaC9iQ6hJWc99DtDoo2ucwHQYDVR0OBBYEFDcV/xxzq2eE
WQMQ0so/S2WbardiMA4GA1UdDwEB/wQEAwIFoDAMBgNVHRMBAf8EAjAAMB0GA1Ud
JQQWMBQGCCsGAQUFBwMBBggrBgEFBQcDAjBPBgNVHSAESDBGMDoGCysGAQQBsjEB
AgIHMCswKQYIKwYBBQUHAgEWHWh0dHBzOi8vc2VjdXJlLmNvbW9kby5jb20vQ1BT
MAgGBmeBDAECATBUBgNVHR8ETTBLMEmgR6BFhkNodHRwOi8vY3JsLmNvbW9kb2Nh
LmNvbS9DT01PRE9SU0FEb21haW5WYWxpZGF0aW9uU2VjdXJlU2VydmVyQ0EuY3Js
MIGFBggrBgEFBQcBAQR5MHcwTwYIKwYBBQUHMAKGQ2h0dHA6Ly9jcnQuY29tb2Rv
Y2EuY29tL0NPTU9ET1JTQURvbWFpblZhbGlkYXRpb25TZWN1cmVTZXJ2ZXJDQS5j
cnQwJAYIKwYBBQUHMAGGGGh0dHA6Ly9vY3NwLmNvbW9kb2NhLmNvbTA/BgNVHREE
ODA2ghdhcHBzLWxhbnphbGxhbWFzLmNvbS5hcoIbd3d3LmFwcHMtbGFuemFsbGFt
YXMuY29tLmFyMA0GCSqGSIb3DQEBCwUAA4IBAQALiZPuk5T62fHPnuEELWDtjy1n
iOqnr29DTUF++xwVVMmRJ1HZk+xIV/YaL6gnwIEXD7TJ2cCGKvzN9g7+/aNQCw3T
IugC5UB95S9i/Z6hpacT6Pon8yYI9l9CAJP5AFQb0KljJUcqqetVPqyknrp4L5Qd
ViPoTvPp3s1DWFmXt5UWmVBzFUfPfmpbF6WDOIGc81Ph4l6Cltyul52druHZM0dE
ZZC94aK3AWGhncfSMlG+LmiOTwPnSeM4I0y/hvAf8oxe9M27uaeZ8L7ew5mxwsyu
LmVJzvc0PybZOOgPTjawE6afKSO9r0qscsmsUFhvplg7cWHP/T0TA5cjQ2ES
-----END CERTIFICATE-----
    ";

        AndroidHttpsHelper.AddCertificate(cert1);
        mInitialized = true;
    }

	// Use this for initialization
	void Start ()
    {
	    if(mInitialized == false)
        {
            Debug.LogError("Initialization failed. Default WWW class is used.");
        }
		StartCoroutine(Download("https://apps-lanzallamas.com.ar/unity-server/usuarios"));
	}

    IEnumerator Download(string url)
    {
        WWW wwwget = new WWW(url);
        yield return wwwget;

        mResponse += "URL: " + url;
        if (!string.IsNullOrEmpty(wwwget.error))
        {
            mResponse += "\n<color=#FF0000>Error: " + wwwget.error + "</color>";
            mResponse += "\nResponse: " + wwwget.text;
        }else
        {
            string response = wwwget.text;
            if (response.Length > 200)
                response = response.Substring(0, 200);
            mResponse += "\n<color=#00FF00>Connection successfull!</color> ";
            mResponse += "\nResponse: " + response;
        }

        mResponse += "\n\n";
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if (mInitialized)
        {
            GUILayout.Label("Plugin initialized.");
        }else
        {
            GUILayout.Label("Failed to initialize the plugin.");
        }

        if (mResponse == null)
        {
            GUILayout.Label("No server response.");
        }
        else
        {
            GUILayout.Label(mResponse);
        }
        GUILayout.EndVertical();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
