code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h
start:	jmp begin
begin:	mov bl, 3ah ;�N16�i��3a�ƭȷh��bl�@��ƭȼȦs��
		mov cl, 4	;�N4�h��cl�@��8�줸�p�ƼȦs��
		mov dl, bl	;�Nbl�h��dl�r���Ȧs��
		shr dl, cl	;�Ndl�첾c1��
		add dl, 30h	;�Ndl + 30h
		cmp dl, '9' ;���
		jbe show1	;�p�G�p�󵥩����show
		add dl, 7	;��l�� dl + 7
show1:	mov ah, 2	;2�h����ah�ǳƿ�X(2=��ܦr��)
		int 21h		;�{�ǿ�X
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
