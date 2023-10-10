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
	printf("Starting...\n");
	
	// For now, just send this to clients
	const char* message = "\nHOLY GUACAMOLE!\n\nYes!  I've just learned what I need to build my very own MUD! :)\n\n";
	
	// Create a TCP socket
	int socketInfo = socket(AF_INET, SOCK_STREAM, 0);
	
	// Set up a struct with the info we need to tell it which address to connect to
	struct sockaddr_in server;
	server.sin_family = AF_INET;
	server.sin_port = htons(9002);	// port
	server.sin_addr.s_addr = INADDR_ANY;	// address (usually an IP apparently)
	
	// "Bind" the socket to an IP/PORT
	bind(socketInfo, (struct sockaddr*)&server, sizeof(server));
	
	// Listen on that port
	listen(socketInfo, 10);
	
	// This next function call gives us a socket we can send data to.
	// The other 2 parameters are the IP and other stuff about where the connection is coming from.
	// Since we don't need that we can leave them as NULL
	int client = accept(socketInfo, NULL, NULL);
	
	// Send the data (he used sizeof, not strlen btw)
	send(client, message, strlen(message), 0);
	
	close(socketInfo);
	printf("Done.\n");
	return 0;
}
