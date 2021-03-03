#include "stdafx.h"
#include "DateTime.h"

DateTime::DateTime()
{
}

DateTime::~DateTime()
{
}

std::string DateTime::GetNowDate()
{
	struct tm tmDate;
	std::string sNowDate	= "";
	time_t ttNowTime		= time(0);

	localtime_s(&tmDate, &ttNowTime);

	sNowDate += "Year¡G" + std::to_string((1900 + tmDate.tm_year));
	sNowDate += " Month¡G" + std::to_string((1 + tmDate.tm_mon));
	sNowDate += " Day¡G" + std::to_string(tmDate.tm_mday);
	sNowDate += " Time¡G" + std::to_string(tmDate.tm_hour) + ":" +
							std::to_string(1 + tmDate.tm_min) + ":" +
							std::to_string(1 + tmDate.tm_sec) + "\n";
	return sNowDate;
}