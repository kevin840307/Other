code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h

start:	jmp begin
hex_n dw 0ffeeh
begin:	mov dx, hex_n		;直接存入16byte(dh+dl)
		mov cl, 4			;位移用
		xchg dh, dl			;交換位置因為方便輸出
		call print_8bit
		mov dl, dh			;可直接搬移(也可xchg), 因沒要用到了
		call print_8bit
		mov dl, 'h'
		call p_ascii
		mov dl, '='
		call p_ascii


		mov cx, 10000
		mov ax, hex_n
		mov dx, 0
		div cx			;被除數大於255 dx為餘數 ax為商
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

;-----------輸出字元fun-----------------
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


;-----------輸出字元fun-----------------
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

;-----------輸出ASCLLfun-----------------
p_ascii	proc near
		mov ah, 2
		int 21h
		ret
p_ascii	endp
;----------------------------------------
code ENDS
	END start