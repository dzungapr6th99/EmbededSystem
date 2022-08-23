#pragma once
#include<string.h>
#include<stdlib.h>
const char* IoTBegin = "IoT=Begin";
const char* IoTEnd = "IoT=End";
class IoTMessage
{
private:
	const char* _TagMessageType = "1=";
	const char* _TagSeqNum = "2=";
	const char* _TagAccount = "3=";
	const char* _TagHomeID = "4=";
	const char* _TagStatus = "5=";
	const char* _TagOnOff = "6=";
	const char* _TagDeviceType = "7=";
	const char* _TagFanLevel = "8=";
	const char* _TagDeviceID = "9=";
	const char* _TagGapFill = "10=";
	const char* _TagBeginSeqNo = "11=";
	const char* _TagEndSeqNo = "12=";
	const char* _TagPassword = "13=";
	const char* _TagTargetID = "14=";
	const char* _TagSenderID = "15=";
public:
	char** ListString;
	int numbertag;
	/// <summary>
	/// Message gốc
	/// </summary>
	char *MessageRaw;
	/// <summary>
	/// Kiểu Message, tag=1
	/// </summary>
	char* MessageType;
	/// <summary>
	/// Số thứ tự Message, tag=2
	/// </summary>
	int Seqnum;
	/// <summary>
	/// Định danh người dùng, tag=3
	/// </summary>
	char* Account;
	/// <summary>
	/// Định danh nhà thông minh, tag=4
	/// </summary>
    char* HomeID;
	/// <summary>
	/// Trạng thái thiết bị, Tag=5
	/// </summary>
	char* Status;
	/// <summary>
	///  Ra lệnh bật tắt thiết bị, Tag=6
	/// </summary>
	char* OnOff;
	/// <summary>
	/// Loại thiết bị, tag=7
	/// </summary>
	char* DeviceType;
	/// <summary>
	/// Số gió của quạt, tag=8
	/// </summary>
	char* FanLevel;
	/// <summary>
	/// Định dannh thiết bị, tag=9
	/// </summary>
	char* DeviceID;
    /// <summary>
    /// Cờ Gapfill, Tag=10
    /// </summary>
	char* Gapfill;
	/// <summary>
	/// Giá trị đầu tiên của Message bị mất, tag=11
	/// </summary>
	int BeginSeqNo;
	/// <summary>
	/// Giá trị cuối của Mesage bị mất, tag=12
	/// </summary>
	int EndSeqNo;
	/// <summary>
	/// Mật khẩu
	/// </summary>
	char* Password;
	/// <summary>
	/// Id bên nhận
	/// </summary>
	char* TargetID;
	/// <summary>
	/// ID bên gửi
	/// </summary>
	char* SenderID;
private:

	int Indexof(char* src, char* String)
	{
		int j = -1;
		int i = 0;
		while (i < strlen(src))
		{
			int k = 0;
			if (src[i] == String[k])
			{
				while (k < strlen(String) && src[i + k] == String[k])
				{
					k++;
				}
				if (k == strlen(String)) j = i;
				else
				{
					continue;
				}
			}
			return j;
		}
	}
	void Split(char* src, char c)
	{
		int count = 0;
		for (int i = 0; i < strlen(src); i++)
		{
			if (MessageRaw[i] == c)
			{
				count++;
			}
		}
		numbertag = count;
		ListString = new char* [count];
		int i = 0;
		int index = 0;
		int flag = 0;
		while (i < 0)
		{
			if (src[i] == c)
			{
				ListString[index] = new char[i - flag];
				for (int k = 0; k < i - flag; k++)
				{
					ListString[index][k] = src[flag + k];
				}
				index++;
				flag = i;
			}
			i++;
		}
	}
	char** _Split(char* src, char c)
	{
		char** ListStringResult;
		int count = 0;
		for (int i = 0; i < strlen(src); i++)
		{
			if (MessageRaw[i] == c)
			{
				count++;
			}
		}
		numbertag = count;
		ListStringResult = new char* [count];
		int i = 0;
		int index = 0;
		int flag = 0;
		while (i < 0)
		{
			if (src[i] == c)
			{
				ListStringResult[index] = new char[i - flag];
				for (int k = 0; k < i - flag; k++)
				{
					ListStringResult[index][k] = src[flag + k];
				}
				index++;
				flag = i;
			}
			i++;
		}
		return ListStringResult;
	}
	bool IsTagExist(char* src, char* item)
	{
		bool result = true;
		for (int i = 0; i < strlen(item); i++)
		{
			if (src[i] != item[i])
			{
				result = false;
				break;
			}
		}
		return result;
	}
public:
	IoTMessage()
	{
		
	}
	IoTMessage(char *String) 
	{
		strcpy(String, MessageRaw);
		Split(String, '&');
		
		for (int i = 0; i < numbertag; i++)
		{
			char** ListStringResult = _Split(ListString[i], '=');
			if (ListStringResult[0] == _TagMessageType)
			{
				MessageType = ListStringResult[1];
				break;
			}
				
			if (ListStringResult[0] == _TagSeqNum)
			{
				Seqnum = atoi(ListStringResult[1]);
				break;
			}
				
			if (ListStringResult[0] == _TagAccount)
			{
				Account = ListStringResult[1];
				break;
			}
			if (ListStringResult[0] == _TagHomeID)
			{
				HomeID = ListStringResult[1];
				break;
			}
			if (ListStringResult[0] == _TagStatus)
			{
				Status = ListStringResult[1];
				break;
			}
			if (ListStringResult[0] == _TagOnOff)
			{
				OnOff = ListStringResult[1]; 
				break;
			}
			if (ListStringResult[0] == _TagDeviceType)
			{
				DeviceType = ListStringResult[1];
				break;
			}
			if (ListStringResult[0] == _TagFanLevel)
			{
				FanLevel = ListStringResult[1];
				break;
			}
			if (ListStringResult[0] == _TagDeviceID)
			{
				DeviceID = ListStringResult[1];
				break;
			}
			if (ListStringResult[0] == _TagGapFill)
			{
				Gapfill = ListStringResult[1];
			}
			if (ListStringResult[0] == _TagBeginSeqNo)
			{
				BeginSeqNo = atoi(ListStringResult[1]);
				break;
			}
			if (ListStringResult[0] == _TagEndSeqNo)
			{
				EndSeqNo = atoi(ListStringResult[1]);
				break;
			}
			if (ListStringResult[0] == _TagTargetID)
			{
				TargetID = ListStringResult[1];
			}
			if (ListStringResult[0] == _TagSenderID)
			{
				SenderID = ListStringResult[1];
			}

		}

	}
	char* Build()
	{
		MessageRaw = new char[200];
		strcat(MessageRaw, IoTBegin);
		strcat(MessageRaw, "&");
		if (MessageType != NULL)
		{
			strcat(MessageRaw, _TagMessageType);
			strcat(MessageRaw, MessageType);
			strcat(MessageRaw, "&");
		}
		if(Seqnum != NULL)
		{
			strcat(MessageRaw, _TagSeqNum);
			char* buffer = new char[7];
			buffer = itoa(Seqnum, buffer,10);
			strcat(MessageRaw, buffer);
			strcat(MessageRaw, "&");
		}
		if(Account != NULL)
		{
			strcat(MessageRaw, _TagAccount);
			strcat(MessageRaw, Account);
			strcat(MessageRaw, "&");
		}
		if(HomeID != NULL)
		{
			strcat(MessageRaw, _TagHomeID);
			strcat(MessageRaw, HomeID);
			strcat(MessageRaw, "&");
		}
		if(Status != NULL)
		{
			strcat(MessageRaw, _TagStatus);
			strcat(MessageRaw, Status);
			strcat(MessageRaw, "&");
		}
		if (OnOff != NULL)
		{
			strcat(MessageRaw, _TagOnOff);
			strcat(MessageRaw, OnOff);
			strcat(MessageRaw, "&");
		}
		if (DeviceType!= NULL)
		{
			strcat(MessageRaw, _TagDeviceType);
			strcat(MessageRaw, DeviceType);
			strcat(MessageRaw, "&");
		}
		if(FanLevel != NULL)
		{
			strcat(MessageRaw, _TagFanLevel);
			strcat(MessageRaw, FanLevel);
			strcat(MessageRaw, "&");
		}
		if(DeviceID != NULL)
		{
			strcat(MessageRaw, _TagDeviceID);
			strcat(MessageRaw, DeviceID);
			strcat(MessageRaw, "&");
		}
		if(Gapfill != NULL)
		{
			strcat(MessageRaw, _TagGapFill);
			strcat(MessageRaw, Gapfill);
			strcat(MessageRaw, "&");
		}
		if(BeginSeqNo != NULL)
		{
			strcat(MessageRaw, _TagBeginSeqNo);
			char* buffer = new char[7];
			buffer=itoa(BeginSeqNo, buffer, 10);
			strcat(MessageRaw, "&");
		}
		if (EndSeqNo != NULL)
		{
			strcat(MessageRaw, _TagEndSeqNo);
			char* buffer = new char[7];
			buffer = itoa(EndSeqNo, buffer, 10);
			strcat(MessageRaw, buffer);
			strcat(MessageRaw, "&");
		}
		if (Password != NULL)
		{
			strcat(MessageRaw, _TagPassword);
			strcat(MessageRaw, Password);
			strcat(MessageRaw, "&");
		}
		if (TargetID != NULL)
		{
			strcat(MessageRaw, _TagTargetID);
			strcat(MessageRaw, TargetID);
			strcat(MessageRaw, "&");
		}
		if (SenderID != NULL)
		{
			strcat(MessageRaw,_TagSenderID);
			strcat(MessageRaw, SenderID);
			strcat(MessageRaw, "&");
		}
		return MessageRaw;
	}

};

