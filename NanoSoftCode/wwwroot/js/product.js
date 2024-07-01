//JS Swiper
const wrapper = document.querySelector(".wrapper-cat");
const buttons = document.querySelectorAll(".btns-cat > span");

const back = buttons[0];
const forward = buttons[1];

back.onclick = function () {
  wrapper.appendChild(wrapper.firstElementChild);
};
forward.onclick = function () {
    wrapper.insertBefore(wrapper.lastElementChild, wrapper.firstElementChild);
};
function swiperAuto() {
    wrapper.insertBefore(wrapper.lastElementChild, wrapper.firstElementChild);
    setTimeout(swiperAuto, 5000);
};

swiperAuto();