#pragma once

// I'm not putting any include guards in here...
// maybe..

#ifndef REPEAT_FOR
// will repeat the statement(yes, statement) in b a times.
#define REPEAT_FOR(a,b) /*--BEGIN MACRO*/for(int _iterator_in_place_ = 0;_iterator_in_place_<a;_iterator_in_place_++){b}/*---END MACRO--*/
#endif

#ifndef File_Tools
#define File_Tools
#define ONERROR 2
#define ONSUCCESS 0

// implement these down the line.
// prototyping some function calls.
int file_descriptor(char* file_name);

int file_length(int fd);
int file_lines(int fd);

// make sure that thiw ends up working like I want. trying to avoid reworking solved problems.
void write_file(int fd, int length, int* data);


#endif
