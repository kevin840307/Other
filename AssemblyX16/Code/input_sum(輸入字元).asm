code	SEGMENT
		ASSUME cs:code, ds:code
		ORG 100h

start:	jmp short next

digit equ 20
big_d db 0
sma_d db 0
has_chg db 0
v1 db digit + 1 dup(0)
v2 db digit + 1 dup(0)
sum db digit + 1 dup(0)

next:	mov si, OFFSET v1
		call inp_num
		mov big_d, bl

		sub ax, ax
		mov al, byte ptr[big_d]
		mov si, 0
		mov di, WORD ptr[big_d]
		call rev1_num

		mov dl, '+'
		call p_ascll

		mov si, OFFSET v2
		call inp_num
		mov sma_d, bl

		sub ax, ax
		mov al, byte ptr[sma_d]
		mov si, 0
		mov di, WORD ptr[sma_d]
		call rev2_num
		
		call cmp_d
		call add_num

		mov dl, '='
		call p_ascll

		mov si, OFFSET sum
		call print_num

		mov ax, 4c00h
		int 21h

;-----------块计----------------
inp_num proc near
		sub bl, bl
		sub al, al
input:	mov ah, 0
		int 16h
		cmp al, ' '
		je inp_e
		cmp al, '0'
		jae ch_1
		jmp short input
ch_1:	cmp al, '9'
		jbe set_n
		jmp short input
set_n:	mov [si], al
		mov dl, al
		call print
		inc si
		inc bl
		cmp bl, digit - 1
		je inp_e
		jmp short input
inp_e:	ret
inp_num endp
;--------------------------------------

;-----------计fun-----------------
add_num proc near
		cmp byte ptr[has_chg], 0
		je set_1
set_0:	mov si, OFFSET v2
		mov di, OFFSET v1
		jmp set_2
set_1:	mov si, OFFSET v1
		mov di, OFFSET v2
set_2:	mov bx, OFFSET sum
		mov cl, byte ptr[sma_d]
		sub ax, ax
		clc								;秈篨夹C砞﹚0
run_0:	mov al, [si]
		adc al, [di]					;+C篨夹
		aaa								;锣秈 ah秈, al緇计
		mov [bx], al
		inc si
		inc di
		inc bx
		loop run_0

		mov al, [sma_d]
		cmp [big_d], al					;璝计单(⊿Τユ传), 玥ぃ惠膥尿(程计秈)
		je end_1
		mov dl, '+'
		call p_ascll
		mov cl, digit
		sub cl, big_d
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

;-------------v1计は锣fun-----------------
rev1_num proc near
		mov cl, 2
		div cl
		mov cl, al
		dec di
rev1:
		mov bl, [v1 + si]
		mov bh, [v1 + di]
		mov [v1 + si], bh
		mov [v1 + di], bl
		inc si
		dec di
		loop rev1
	ret
rev1_num endp
;----------------------------------------

;-------------v2计は锣fun-----------------
rev2_num proc near
		mov cl, 2
		div cl
		mov cl, al
		dec di
rev2:
		mov bl, [v2 + si]
		mov bh, [v2 + di]
		mov [v2 + si], bh
		mov [v2 + di], bl
		inc si
		dec di
		loop rev2
	ret
rev2_num endp
;----------------------------------------

;-----------ゑ耕ㄢ计计fun---------
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


;-----------块计+砞﹚bl计fun---------
print_num proc near
		add si, digit - 1		;+digit簿程蔼计
		mov cl, digit
g_dig:	mov al, [si]
		and al, 0fh				;綛竛
		cmp al, 0				;耞[si]锣byte琌单0 琌杠计-1
		jbe d_dig
		cmp al, 9				;耞[si]锣byte琌ぃ9 琌杠计-1
		ja d_dig
		jmp set_d
d_dig:	dec si
		dec cl
		jmp g_dig
set_d:	mov bl, cl
call_p:	mov dl, [si]
		push cx
		call print
		pop cx
		dec si
		loop call_p
		ret
print_num	endp
;----------------------------------------


;-----------块じfun-----------------
print_8bit proc near
		mov cl, 4
		mov bl, dl
		shr dl, cl
		call print
		mov dl, bl
		and dl, 0fh
		call print
		ret
print_8bit	endp
;----------------------------------------


;-----------块じfun-----------------
print	proc near
		and dl, 00fh			;綛姜オ娩
		add dl, 48
		cmp dl, '9'
		jbe show
		add dl, 7
show:	mov ah, 2
		int 21h
		ret
print	endp
;----------------------------------------

;-----------块ASCLLfun-----------------
p_ascll	proc near
		mov ah, 2
		int 21h
		ret
p_ascll	endp
;----------------------------------------
code ENDS
	END start