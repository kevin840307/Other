code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h
start:	jmp begin
begin:	mov bl, 3ah ;�N16�i��3a�ƭȷh��bl�@��ƭȼȦs��
		mov cl, 4	;�N4�h��cl�@��8�줸�p�ƼȦs��
		mov dl, bl	;�Nbl�h��dl�r���Ȧs��
		shr dl, cl	;�Ndl�첾c1��
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
