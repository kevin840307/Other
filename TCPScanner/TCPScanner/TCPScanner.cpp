// SocketfnScanners.cpp : �w�q�D���x���ε{�����i�J�I�C
//


#include "stdafx.h"
#include "GSocket.h"
#include <iostream>
#include <thread>
#include "WriteFileData.h"

using namespace std;

void fnWriteOpenPort(const vector<int>& vecOpenPort, const string& sFileNmae, const string& sStart) {
	WriteFileData* wfdWrite = new WriteFileData(sFileNmae);
	for (int iPos = 0; iPos < vecOpenPort.size(); iPos++)
	{
		wfdWrite->fnWriteData(vecOpenPort, sStart);
	}
	delete wfdWrite;
}

void fnWriteError(const string& sFileNmae, const string& sError) {
	WriteFileData* wfdWrite = new WriteFileData(sFileNmae);
	wfdWrite->fnWriteData(sError);
	delete wfdWrite;
}

void fnScannerSocket(const string& k_sIp, const int& k_iStartPort, const int& k_iEndPort, const int& k_iMMSTime)
{
	GSocket* p_gsSocket = NULL;
	try
	{
		p_gsSocket				= new GSocket(k_sIp, k_iMMSTime);
		vector<int> vecOpenPort = p_gsSocket->fnScanner(k_iStartPort, k_iEndPort);
		delete p_gsSocket;
		fnWriteOpenPort(vecOpenPort, "ScannerOpenPort.txt", k_sIp + " �w�}�Һݤf�G");
	}
	catch (SocketException ex)
	{
		fnWriteError("ScannerError.txt", ex.fnGetMessages());
		delete p_gsSocket;
	}
}

void Start()
{
	string  sIp;
	int		iStartPort;
	int		iEndPort;
	int		iMMSTime;

	cout << "Input IP�G";
	cin >> sIp;
	cout << "Enter start port�G";
	cin >> iStartPort;
	cout << "Enter end port�G";
	cin >> iEndPort;
	cout << "Enter mms time�G";
	cin >> iMMSTime;

	thread thSocket(fnScannerSocket, sIp, iStartPort, iEndPort, iMMSTime);
	thSocket.join();
}

int main()
{
	Start();
	system("pause");
	return 0;
}