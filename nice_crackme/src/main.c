#include <stdio.h>
#include <stdlib.h>
int len(char* p){
	if(*p)
		return 1+len(p+1);
	return 0;
}
int check_len(char* p){
	return !(len(p)%4);
}
int check_palindrom4(char *p){
	return (*p==*(p+3))&&(*(p+1)==*(p+2));
}
int check_palindrom_every_four(char *p){
	if(*p)
		return check_palindrom4(p)&check_palindrom_every_four(p+4);
	return 1;
}
int check_all(char *p){
	return check_len(p)&&check_palindrom_every_four(p);
}
void main(){
	char *p=(char*)malloc(sizeof(char)*201);
	int i;
	for(i=1;i<=5;i++){
		printf("Enter password: ");
		fgets(p,201,stdin);
		p[len(p)-1]=0;
		if(check_all(p)){
			puts("CORRECT!");
			exit(0);
		}else{
			printf("WRONG number of attempt: %d\n",i);
		}
	}
	puts("Game Over");
}
