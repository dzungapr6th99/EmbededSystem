#pragma once
#include <string>
using namespace std;
class IoTMessage
{
public:
	string header;
	string message;
	int seqnum;
	int account;
	int homeid;

public:
	string Header()
	{
		return header;
	}
	string Header(string a)
	{
		header = a;
		return a;
	}
	int SeqNum()
	{
		return seqnum;
	}

};

