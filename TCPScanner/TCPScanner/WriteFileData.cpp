#include "stdafx.h"
#include "WriteFileData.h"

WriteFileData::WriteFileData(const std::string& sFileName)
{
	g_sFileName = sFileName;
}

WriteFileData::~WriteFileData()
{
}

void WriteFileData::fnWriteData(const std::string& sData)
{
	std::ofstream ofsWrite;
	ofsWrite.open(g_sFileName, std::ios::app);
	ofsWrite << sData << std::endl;
	ofsWrite.close();
}

void WriteFileData::fnWriteData(const std::vector<int>& vecData, const std::string& sStart)
{
	std::ofstream ofsWrite;
	ofsWrite.open(g_sFileName, std::ios::out);
	for (int iPos = 0; iPos < vecData.size(); iPos++)
	{
		ofsWrite << sStart << vecData[iPos] << std::endl;
	}
	ofsWrite.close();
}
