code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h

start:	jmp short begin
hex_n dw ?
begin:	mov bx, 0
next:	mov ah, 0
		int 16h
		cmp al, '='
		je cmpu
		cmp al, '0'
		jb next
		cmp al, '9'
		ja ch_asc
		sub al, 30h
		jmp short add_n
ch_asc: and al, 0dfh		;����j�g�^��r1011 1111�B�n��2�� �]���p�g�r�����t32
		cmp al, 'A'
		jb next
		cmp al, 'F'
		ja next
		sub al, 37h
add_n:	mov dl, al			;al�h��dl�ǳ����
		push ax				;��ܮɷ|�ϥΨ�ah�ҥH���N�쥻��push�~���|�v�T
		call print			;��X
		pop ax				

		mov cl, 4
		shl bx, cl			;�����첾4bit
		cbw					;al 8�줸��16�줸�h�Jax
		add bx, ax

		mov dh,bh			;�}�l�ˬd�O�_�U����J�|�j��FFFF
		cmp dh, 00fh
		ja cmpu
		jmp next
cmpu:	mov dl, 'h'
		call p_ascii
		mov dl, '='
		call p_ascii
		
		mov cx, 10000
		mov dx, 0
		mov ax, bx
		div cx
		xchg ax, dx			;�N�l�Ʃ�bax �ө�bdx
		mov bx, ax			;�l�Ʀs�Jbx�Ȧs�����U�@�ӭp��
		call print

		mov cx, 1000
		mov dx, 0
		mov ax, bx
		div cx
		xchg ax, dx			;�N�l�Ʃ�bax �ө�bdx
		mov bx, ax			;�l�Ʀs�Jbx�Ȧs�����U�@�ӭp��
		call print
		
		mov cx, 100
		mov dx, 0
		mov ax, bx
		div cx
		xchg ax, dx			;�N�l�Ʃ�bax �ө�bdx
		mov bx, ax			;�l�Ʀs�Jbx�Ȧs�����U�@�ӭp��
		call print

		mov cx, 10
		mov dx, 0
		mov ax, bx
		div cx
		xchg ax, dx			;�N�l�Ʃ�bax �ө�bdx
		mov bx, ax			;�l�Ʀs�Jbx�Ȧs�����U�@�ӭp��
		call print

		mov dx, bx
		call print

		mov ax, 4c00h
		int 21h

;-----------��X�r��fun-----------------
print_8bit	proc near
		mov bl, dl
		shr dl, cl
		call print
		mov dl, bl
		and dl, 0fh
		call print
		ret
print_8bit	endp
;----------------------------------------


;-----------��X�r��fun-----------------
print	proc near
		and dl, 00fh			;�B������
		add dl, 48
		cmp dl, '9'
		jbe show
		add dl, 7
show:	mov ah, 2
		int 21h
		ret
print	endp
;----------------------------------------

;-----------��XASCLLfun-----------------
p_ascii	proc near
		mov ah, 2
		int 21h
		ret
p_ascii	endp
;----------------------------------------
code ENDS
	END start