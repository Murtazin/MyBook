// //Открывает окно входа

// $('.nav__btn-login').on('mousedown', function () {
// 	$('#login-layout').show(250);
// 	$("html,body").css("overflow-y", "hidden");
// });

// //Закрывает окно входа

// $('#login-btn_close').on('mousedown', function () {
// 	$('#login-layout').hide(250);
// 	$("html,body").css("overflow-y", "visible");
// });

// //Закрывает окно регистрации

// $('#registration-btn_close').on('mousedown', function () {
// 	$('#register-layout').hide(250);
// 	$("html,body").css("overflow-y", "visible");
// });

// //Переход на регистрацию

// $('#transition-register').on('mousedown', function () {
// 	$('#login-layout').hide();
// 	$('#register-layout').show();
// });

// //Переход на авторизацию

// $('#transition-login').on('mousedown', function () {
// 	$('#register-layout').hide();
// 	$('#login-layout').show();
// });

//Проверка на ввод символов в поле поиска

$(document).ready(function () {
	var btnCancel = $('#search-cancel');
	$('#search-input').on('keyup input', function () {
		var value = this.value;
		btnCancel.show();
		btnCancel.filter(':contains("' + value + '")').hide();

	});
});

//Очищает поле поиска

$('#search-cancel').on('mousedown', function () {
	$('#search-input').val('');
	$('#search-cancel').hide();
});

//Swiper slider

const slider = document.querySelector('.swiper-container');

new Swiper(slider, {
	slidesPerView: 8.2,
	spaceBetween: 16,
	navigation: {
		nextEl: '.slider-button-next',
		prevEl: '.slider-button-prev',
	},
	grabCursor: true,
	freeMode: true,
});
