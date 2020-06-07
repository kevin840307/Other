code SEGMENT
	ASSUME cs:code,ds:code
	ORG	100h
start: jmp begin
msg DB 'Hello, World!$'
msg2 DB 'CC!$'
num1 BYTE 1,2,3
begin: mov dx, OFFSET msg
	mov ah,9
	int 21h
	mov dx, OFFSET msg2
	mov ah,9
	int 21h
	mov ax,4c00h
	int 21h
code ENDS
	END	start