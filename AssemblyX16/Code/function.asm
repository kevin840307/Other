code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h
start:	jmp begin
begin:	mov bl, 3ah ;將16進位3a數值搬到bl一般數值暫存器
		mov cl, 4	;將4搬到cl一個8位元計數暫存器
		mov dl, bl	;將bl搬到dl字元暫存器
		shr dl, cl	;將dl位移c1次
		call print
		mov dl, bl
		and dl, 0fh
		call print
		mov ax, 4c00h
		int 21h
print	proc near
		add dl, 48
		cmp dl, '9'
		jbe show
		add dl, 7
show:	mov ah, 2
		int 21h
		ret
print	endp
code ENDS
	END start
