code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h

start:	jmp short next
digit equ 12
big_d db 0
sma_d db 0
has_chg db 0
v1 db 7, 3, 6, 5, 2, 0, 7, 0, 0, 0, 1, 0
v2 db 4, 2, 5, 1, 1, 6, 8, 0, 5, 0, 0, 0
sum db digit + 1 dup(0)
next:	mov si, OFFSET v1
		call print_num
		mov big_d, bl

		mov dl, '+'
		call p_ascii

		mov si, OFFSET v2
		call print_num
		mov sma_d, bl

		mov dl, '='
		call p_ascii

		call cmp_d


		call add_num

		mov si, OFFSET sum
		call print_num

		

		mov ax, 4c00h
		int 21h
;-----------數字相加fun-----------------
add_num proc near
		cmp has_chg, 0
		je set_1
set_0:	mov si, OFFSET v2
		mov di, OFFSET v1
		jmp set_2
set_1:	mov si, OFFSET v1
		mov di, OFFSET v2
set_2:	mov bx, OFFSET sum
		mov cl, byte ptr[sma_d]
		sub ax, ax
		clc							;進位旗標C設定為0
run_0:	mov al, [si]
		adc al, [di]				;相加+C旗標
		aaa							;轉十進位 ah進位, al餘數
		mov [bx], al
		inc si
		inc di
		inc bx
		loop run_0

		mov al, [sma_d]
		cmp [big_d], al					;若位數相等(沒有交換), 則不需繼續加(最大位數進位)
		je end_1

		mov cl, digit
		sub cl, big_d + 1
add_z:	mov al, [si]
		adc al, 0
		aaa
		mov [bx], al
		
		inc bx
		inc si
		loop add_z
		ret

end_1:	mov al, 0
		adc al, 0
		aaa
		mov [bx], al
		inc bx
		ret
add_num	endp
;----------------------------------------

;-----------比較兩數位數的大小fun---------
cmp_d proc near
		cmp big_d, bl
		jb chang
		ret
chang:	mov bl, [big_d]
		mov bh, [sma_d]
		mov sma_d, bl
		mov big_d, bh
		mov has_chg, 1
		ret
cmp_d	endp
;----------------------------------------


;-----------輸出數字+設定bl位數fun---------
print_num proc near
		add si, (digit - 1)			;地址+上digit移至最高位數
		mov cx, digit
g_dig:	cmp byte ptr[si], 0			;判斷取值[si]轉byte是否等於小於0 是的話位數-1
		jbe d_dig
		cmp byte ptr[si], 9			;判斷取值[si]轉byte是否不大於9 是的話位數-1
		ja d_dig
		jmp set_d
d_dig:	dec si
		dec cx
		jmp g_dig
set_d:	mov bl, cl
call_p:	push cx
		mov dl, [si]
		call print
		pop cx
		dec si
		loop call_p
		ret
print_num	endp
;----------------------------------------


;-----------輸出字元fun-----------------
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


;-----------輸出字元fun-----------------
print	proc near
		and dl, 00fh			;遮蔽左邊
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