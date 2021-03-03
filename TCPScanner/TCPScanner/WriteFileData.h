#ifndef WRITE_FILE
#define WRITE_FILE
#include <fstream>
#include <string>
#include <vector>
class WriteFileData
{
public:
	WriteFileData(const std::string& sFileName);
	~WriteFileData();

	void fnWriteData(const std::string& sData);
	void fnWriteData(const std::vector<int>& vecData, const std::string& sStart);
private:
	std::string	g_sFileName;
};

#endif