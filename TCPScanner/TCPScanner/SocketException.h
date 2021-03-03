#ifndef IP_EXCEPRION
#define IP_EXCEPRION
#include <string>

class SocketException : std::exception
{
public:
	SocketException(const std::string& sMessage);
	~SocketException();

	const std::string fnGetMessages();
private:
	std::string g_sMessage;
};
#endif