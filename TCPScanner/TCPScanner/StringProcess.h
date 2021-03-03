#ifndef STRING_PROCESS
#define STRING_PROCESS
#include <string>
#include <vector>
class StringProcess
{
public:
	StringProcess();
	~StringProcess();

	std::vector<std::string> fnSplit(const std::string& k_sData, const char& k_cKey);
private:

};

#endif