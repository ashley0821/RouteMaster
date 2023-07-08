const bigShow = document.querySelector(".layout-wrapper");
bigShow.insertAdjacentHTML('beforeend', carousel);

$(".area").on('click', function (e) {
    e.stopPropagation();
});
$(".big").on('click', function (e) {
    $(this).removeClass('bigShow');
    e.stopPropagation();
})

