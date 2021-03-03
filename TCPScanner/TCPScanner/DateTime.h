#ifndef DATE_TIME
#define DATE_TIME
#include <ctime>
#include <string>
class DateTime
{
public:
	DateTime();
	~DateTime();

	std::string GetNowDate();
private:
};

#endif