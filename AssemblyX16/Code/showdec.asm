code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h

start:	jmp begin
hex_n dw 0ffeeh
begin:	mov dx, hex_n		;�����s�J16byte(dh+dl)
		mov cl, 4			;�첾��
		xchg dh, dl			;�洫��m�]����K��X
		call print_8bit
		mov dl, dh			;�i�����h��(�]�ixchg), �]�S�n�Ψ�F
		call print_8bit
		mov dl, 'h'
		call p_ascii
		mov dl, '='
		call p_ascii


		mov cx, 10000
		mov ax, hex_n
		mov dx, 0
		div cx			;�Q���Ƥj��255 dx���l�� ax����
		xchg dx, ax
		mov bx, ax
		call print

		mov cx, 1000
		mov ax, bx
		mov dx, 0
		div cx
		xchg dx, ax
		mov bx, ax
		call print

		mov cx, 100
		mov ax, bx
		mov dx, 0
		div cx
		xchg dx, ax
		mov bx, ax
		call print

		mov cx, 10
		mov ax, bx
		mov dx, 0
		div cx
		xchg dx, ax
		mov bx, ax
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