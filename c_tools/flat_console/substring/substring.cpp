// substring.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#define BLOCK sizeof(char)
#define PRINT(b) printf(b)
#define DEBUG
//
#define INTSIZE sizeof(int)
#define DOUBLESIZE sizeof(double)
#define FLOATSIZE sizeof(float)
//

#define UNTIL_NULL(c,d) (void* e = *d; while(e!='\0'){ c++; e=*d+c; })
#define DO_UNTIL_NULL(c,d) (void* e = *d; while(d!='\0'){ d = e+1; d=*e; c })


// currently should just print all of th strings.
// substring - will take input and emit output.
int main(int argc, char *argv[])
{
#ifdef DEBUG
	if (sizeof(char) == BLOCK)
	{
		PRINT("SUCCESS! sizeof == block! ");
	}
	else
	{
		PRINT("FAILURE!");
	}
#endif
	DO_UNTIL_NULL(
		char* dd = &argv[argc];
		{			
			const char m = *dd;
			print(m);
		}
		,
			dd;
		)
	// these were kind of poor construction.
	/*
	char *a = argv[1];
	char *b = argv[2];
	int Con_A = *a;
	int Con_B = *b; // implicitly converting into an integer... need to convert to a readable character.
	int ct = 0;
	void* p = (argv[argc])+1; // hopefully this will give me a pointer to the address out at argv[argc]
	printf("Entering loop");
	REPEAT_FOR(14000000, {
		// what needs to be iterated:
		const char dog = ((char)p+ct);
	    const char *odg = &dog;
	    printf(odg);
		//printf("I'm in a loop");
	ct++;
		})
		printf("Unfortunate");
		*/
	
    return 0;
}

void altmain()
{
	// I want to generate arbitrary opcodes.
}

