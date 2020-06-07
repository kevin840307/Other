code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h
start:	jmp begin
begin:	mov bl, 3ah ;將16進位3a數值搬到bl一般數值暫存器
		mov cl, 4	;將4搬到cl一個8位元計數暫存器
		mov dl, bl	;將bl搬到dl字元暫存器
		shr dl, cl	;將dl位移c1次
		add dl, 30h	;將dl + 30h
		cmp dl, '9' ;比對
		jbe show1	;如果小於等於跳到show
		add dl, 7	;其餘的 dl + 7
show1:	mov ah, 2	;2搬移到ah準備輸出(2=顯示字元)
		int 21h		;程序輸出
		mov dl, bl
		and dl, 0fh
		add dl, 48
		cmp dl, '9'
		jbe show2
		add dl, 7
show2:	mov ah, 2
		int 21h
		mov ax, 4c00h
		int 21h
code ENDS
	END start
