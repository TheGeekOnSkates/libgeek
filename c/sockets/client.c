#include <stdio.h>
#include <stdint.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>

// Tutorial: https://www.youtube.com/watch?v=LtXEMwSG5-8
int main() {
	// Create a TCP socket
	int socketInfo = socket(AF_INET, SOCK_STREAM, 0);
	
	// Set up a struct with the info we need to tell it which address to connect to
	struct sockaddr_in server;
	server.sin_family = AF_INET;
	server.sin_port = htons(9002);	// port
	server.sin_addr.s_addr = INADDR_ANY;	// address (usually an IP apparently)
	
	// Start connection
	int result = connect(socketInfo, (struct sockaddr*)&server, sizeof(server));
	if (result == -1) {
		perror("Connection error");
		return 0;
	}
	
	// Get the response
	char data[512];
	recv(socketInfo, &data, 512, 0);
	
	// Print it
	printf("Received: %s\n", data);
	
	// Close the socket
	close(socketInfo);
	
	return 0;
}
