// flat_file_console.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

// define the basic char modes...
const char r = 'r';
const char w = 'w';
const char a = 'a';

// okay - defining it this way seems to do just fine.
int file_descriptor(const char *file_path) 
{ 	
	// I guess I'll play along with windows.
	// use fopen when on linux/
	void* m = fopen(file_path, &r);
	return (int)&m;
};





// how do we want to call this...
// we'll push everything to either stdout/stderr;
// we can redirect output once I've gotten more of a handle on what I want.
int main(int argc, char *argv)
{
	// argc contains the number of args...
	const char *out_file= "./test_file";

	int q = file_descriptor(out_file);
	const char r = (char)&q;
	
	// this is working - it's bizzare, being a file descriptor, but I don think that I opened it.
	// this is working - it's bizzare, being a file descriptor, but I don think that I opened it.
	printf(&r); // alright - get the address and implicity point. I should see a file desciptor on screen.
	printf("So far, it looks like I've been able to write the memory address.");
	
	// we'll need to poke around a bit more... //fprintf( FILE* file_descriptor(out_file), argv);


    return 0;
}

