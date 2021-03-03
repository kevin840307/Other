#include "stdafx.h"
#include "StringProcess.h"

StringProcess::StringProcess()
{}

StringProcess::~StringProcess()
{}

std::vector<std::string> StringProcess::fnSplit(const std::string& k_sData, const char& k_cKey)
{
	std::vector<std::string> vecSplit;
	for (int iPos = 0; iPos < k_sData.size(); iPos++)
	{
		std::string sSplit = "";
		while (iPos < k_sData.size() && k_sData[iPos] != '.')
		{
			sSplit += k_sData[iPos];
			iPos++;
		}
		vecSplit.push_back(sSplit);
	}
	return vecSplit;
}