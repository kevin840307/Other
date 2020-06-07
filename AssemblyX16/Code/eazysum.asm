code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h

start:	jmp short next
digit equ 6
v1 db 7, 3, 6, 5, 9, 0
v2 db 4, 2, 5, 1, 3, 0
sum db digit + 1 dup(?)
next:	mov si, OFFSET v1
		call print_num

		mov dl, '+'
		call p_ascii

		mov si, OFFSET v2
		call print_num

		mov dl, '='
		call p_ascii

		call add_num
		mov si, OFFSET sum
		call print_num

		mov ax, 4c00h
		int 21h
;-----------�Ʀr�ۥ[fun-----------------
add_num proc near
		mov si, OFFSET v1
		mov di, OFFSET v2
		mov bx, OFFSET sum
		mov cx, digit
		sub ax, ax
		clc
run_0:	mov al, [si]
		adc al, [di]
		aaa

		
		;push dx
		;push ax
		;PUSHF
		;mov dl, al
		;call print
		;POPF
		;pop ax
		;pop dx

		mov [bx], al
		inc si
		inc di
		inc bx
		dec cx
		jcxz end_1			;cx=0�h���D
		jmp run_0

end_1:	adc al, 0
		mov [bx], al

		ret
add_num	endp
;----------------------------------------
;-----------��X�Ʀrfun-----------------
print_num proc near
		add si, (digit - 1)			;�a�}+�Wdigit���̰ܳ����
		mov cx, digit
		jmp g_dig
g_dig:	cmp byte ptr[si], 0			;�P�_����[si]��byte�O�_����p��0 �O���ܦ��-1
		jbe d_dig
		cmp byte ptr[si], 9			;�P�_����[si]��byte�O�_���j��9 �O���ܦ��-1
		ja d_dig
		jmp call_p
d_dig:	dec si
		dec cx
		jmp g_dig
call_p:	mov dl, [si]
		call print
		dec si
		loop call_p
		ret
print_num	endp
;----------------------------------------


;-----------��X�r��fun-----------------
print_8bit proc near
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