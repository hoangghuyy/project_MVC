$(() => {
    if ($(window).width() >= 1080) {
        $(window).scroll(function () {
            let currentScroll = $(window).scrollTop();
            if (currentScroll >= 100)
                $("header").addClass("mini");
            else
                $("header").removeClass("mini");

            let popuplr = $('.popup-left, .popup-right');
            if (popuplr.length > 0) {
                let posBanner = $('.slide-index').height() + $('.slide-index').offset().top - 100;
                if (currentScroll >= posBanner) {
                    if (popuplr.hasClass('hide'))
                        popuplr.removeClass('hide');
                } else {
                    if (!popuplr.hasClass('hide'))
                        popuplr.addClass('hide');
                }
            }
        });
        let currentScroll = $(window).scrollTop();
        if (currentScroll >= 100)
            $("header").addClass("mini");
        else
            $("header").removeClass("mini");

        $('header .more-module').on('click', function () {
            $(this).css('display', 'none');
            $('header .hide-module').css('display', 'flex');
            let itemMenu = $('.list-module .item-menu');
            if (itemMenu.hasClass('hide')) {
                itemMenu.removeClass('hide');
            }
        })
    }
    $('.scroll-top').click(() => {
        let body = $("html, body");
        body.stop().animate({ scrollTop: 0 }, 500, 'swing', function () {
        });
    })
    if ($(window).width() < 1079) {
        let iconDownFooter = $('footer .footer-top .item-footer .icon-down-footer')
        let listMenuFooter = $('footer .footer-top .item-footer .list-menu');
        iconDownFooter.on('click', function () {
            let listMenu = $(this).parent().next();
            iconDownFooter.not($(this)).removeClass('active');
            listMenuFooter.not(listMenu).slideUp();
            $(this).toggleClass('active');
            listMenu.slideToggle();
        })
    }

    $('header .search_bottom').on('click', function () {
        let formsearch = $('.form_search_bottom');
        formsearch.toggleClass('active');
        formsearch.find('input').focus();
    })

    $('header .cart-bottom').on('click', function () {
        let formsearch = $('.form_search_bottom');
        formsearch.toggleClass('active');
        formsearch.find('input').focus();
    })

    let menuheader = $('header .menu-mobile');
    let overlay = $('.overlay');
    let menu = $('.menu-mb');
    menuheader.on('click', function () {
        $(this).toggleClass('active');
        overlay.toggleClass('active');
        menu.toggleClass('active');

    });

    overlay.on('click', function () {
        $(this).removeClass('active');
        menuheader.removeClass('active');
        menu.removeClass('active');
    });

    let icondown = $('.menu-mb .icon-down');
    let submenu = $('.menu-mb .submenu');
    let icondown2 = $('.menu-mb .icon-down-2');
    let submenu2 = $('.menu-mb .submenu-2');
    let icondown3 = $('.menu-mb .icon-down-3');
    let submenu3 = $('.menu-mb .submenu-3');
    toggleMenuMb(icondown, submenu)
    toggleMenuMb(icondown2, submenu2)
    toggleMenuMb(icondown3, submenu3)

    let listCategoryE = $('.home-category-list .list-category');
    if (listCategoryE.length > 0) {
        let elScroll = $('.home-category-list .list-block');
        let itemPrev = $('.item-nav .item-prev');
        let itemNext = $('.item-nav .item-next');
        let wListCategory = listCategoryE.width();
        $('.home-category-list .list-category .item .item-child').css('width', wListCategory / 10 + 'px');
        itemNext.on('click', function () {
            elScroll.animate({
                scrollLeft: elScroll.scrollLeft() + wListCategory
            }, 1000);
        })

        itemPrev.on('click', function () {
            elScroll.animate({
                scrollLeft: elScroll.scrollLeft() - wListCategory
            }, 1000);
        })
    }

    // slide
    $(".slide-index .owl-carousel").owlCarousel({
        items: 1,
        nav: 1,
        dots: 1,
        autoplay: 1,
        autoplayHoverPause: 1,
        loop: 1,
        navText: [
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-left" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"/></svg>',
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-right" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"/></svg>',
        ],
        autoplayTimeout: 5000,
        smartSpeed: 1000,
        onInitialized: function () {
            $(".slide-index .owl-nav .owl-next").attr('role', 'button').attr('aria-label', "next");
            $(".slide-index .owl-nav .owl-prev").attr('role', 'button').attr('aria-label', "prev");
            $(".slide-index .owl-dots .owl-dot").attr('role', 'button').attr('aria-label', "dot");
            $('.slide-index .owl-item:not(.active) .item img').attr('loading', 'lazy');
            $(".slide-index .disabled").remove();
        }
    });

    $(".silde-product-hot .owl-carousel").owlCarousel({
        items: 5,
        nav: 1,
        dots: 0,
        autoplay: 1,
        autoplayHoverPause: 1,
        loop: 1,
        margin: 15,
        navText: [
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-left" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"/></svg>',
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-right" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"/></svg>',
        ],
        autoplayTimeout: 5000,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 2,
                dots: 0,
            },
            578: {
                items: 3,
                dots: 0,
            },
            992: {
                items: 4,
            },
            1200: {
                items: 5,
            },
        },
        onInitialized: function () {
            $(".silde-product-hot .owl-nav .owl-next").attr('role', 'button').attr('aria-label', "next");
            $(".silde-product-hot .owl-nav .owl-prev").attr('role', 'button').attr('aria-label', "prev");
            $('.silde-product-hot .owl-item:not(.active) .item img').attr('loading', 'lazy');
            $(".silde-product-hot .disabled").remove();
        }
    });

    $(".silde-product-other .owl-carousel").owlCarousel({
        items: 4,
        nav: 1,
        dots: 0,
        autoplay: 1,
        autoplayHoverPause: 1,
        loop: 1,
        margin: 15,
        navText: [
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-left" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"/></svg>',
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-right" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"/></svg>',
        ],
        autoplayTimeout: 5000,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 2,
            },
            578: {
                items: 3,
            },
            992: {
                items: 4,
            },
            1200: {
                items: 5,
            },
        },
        onInitialized: function () {
            $(".silde-product-other .owl-nav .owl-next").attr('role', 'button').attr('aria-label', "next");
            $(".silde-product-other .owl-nav .owl-prev").attr('role', 'button').attr('aria-label', "prev");
            $('.silde-product-other .owl-item:not(.active) .item img').attr('loading', 'lazy');
            $(".silde-product-other .disabled").remove();
        }
    });

    let slideThumbElement = $('.gallery-thumbs');
    if (slideThumbElement.length > 0) {
        let itemthumb = $('.slide-image-product .mySwiper .swiper-slide')
        itemthumb.click(function () {
            if ($(this).next() && !$(this).next().hasClass('swiper-slide-visible')) {
                swiper.slideNext();
            }
            if ($(this).prev() && !$(this).prev().hasClass('swiper-slide-visible')) {
                swiper.slidePrev();
            }
        })
    }

    $(".slide-album-product .owl-carousel").owlCarousel({
        items: 1,
        nav: 1,
        dots: 0,
        autoplay: 1,
        autoplayHoverPause: 1,
        loop: 0,
        margin: 0,
        navText: [
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-left" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"/></svg>',
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-right" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"/></svg>',
        ],
        autoplayTimeout: 5000,
        smartSpeed: 1000,
        onInitialized: function () {
            $(".slide-album-product .owl-nav .owl-next").attr('role', 'button').attr('aria-label', "next");
            $(".slide-album-product .owl-nav .owl-prev").attr('role', 'button').attr('aria-label', "prev");
            $('.slide-album-product .owl-item:not(.active) .item img').attr('loading', 'lazy');
            $(".slide-album-product .owl-dots .disabled").remove();
        }
    });

    $(".slide-module-product .owl-carousel").owlCarousel({
        items: 8,
        nav: 1,
        dots: 0,
        autoplay: 1,
        autoplayHoverPause: 1,
        loop: 1,
        margin: 0,
        navText: [
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-left" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"/></svg>',
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-right" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"/></svg>',
        ],
        autoplayTimeout: 5000,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1,
                nav: 0,
                dots: 0,
            },
            578: {
                items: 2,
                nav: 0,
                dots: 0,
            },
            992: {
                items: 5,
            },
            1200: {
                items: 10,
            },
        },
        onInitialized: function () {
            $(".slide-module-product .owl-nav .owl-next").attr('role', 'button').attr('aria-label', "next");
            $(".slide-module-product .owl-nav .owl-prev").attr('role', 'button').attr('aria-label', "prev");
            $('.slide-module-product .owl-item:not(.active) .item img').attr('loading', 'lazy');
            $(".slide-module-product .disabled").remove();
        }
    });

    $(".slide-partner .owl-carousel").owlCarousel({
        items: 5,
        nav: 1,
        dots: 0,
        autoplay: 1,
        autoplayHoverPause: 1,
        loop: 1,
        margin: 15,
        navText: [
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-left" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"/></svg>',
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-right" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"/></svg>',
        ],
        autoplayTimeout: 5000,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 3,
            },
            578: {
                items: 4,
            },
            1200: {
                items: 5,
            },
        },
        onInitialized: function () {
            $(".slide-partner .owl-nav .owl-next").attr('role', 'button').attr('aria-label', "next");
            $(".slide-partner .owl-nav .owl-prev").attr('role', 'button').attr('aria-label', "prev");
            $('.slide-partner .owl-item:not(.active) .item img').attr('loading', 'lazy');
            $(".slide-partner .disabled").remove();
        }
    });
    $(".list-news-other .owl-carousel").owlCarousel({
        items: 3,
        nav: 1,
        dots: 0,
        autoplay: 1,
        autoplayHoverPause: 1,
        loop: 1,
        margin: 15,
        navText: [
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-left" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"/></svg>',
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-right" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"/></svg>',
        ],
        autoplayTimeout: 5000,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1,
                nav: 0,
                dots: 0,
            },
            578: {
                items: 2,
                nav: 0,
                dots: 0,
            },
            992: {
                items: 3,
            }
        },
        onInitialized: function () {
            $(".list-news-other .owl-nav .owl-next").attr('role', 'button').attr('aria-label', "next");
            $(".list-news-other .owl-nav .owl-prev").attr('role', 'button').attr('aria-label', "prev");
            $('.list-news-other .owl-item:not(.active) .item img').attr('loading', 'lazy');
            $(".list-news-other .disabled").remove();
        }
    });
    $(".slide-banner-ads-product .owl-carousel").owlCarousel({
        items: 3,
        nav: 1,
        dots: 0,
        autoplay: 1,
        autoplayHoverPause: 1,
        loop: 1,
        margin: 15,
        navText: [
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-left" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M11.354 1.646a.5.5 0 0 1 0 .708L5.707 8l5.647 5.646a.5.5 0 0 1-.708.708l-6-6a.5.5 0 0 1 0-.708l6-6a.5.5 0 0 1 .708 0z"/></svg>',
            '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" fill="currentColor" class="bi bi-chevron-right" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M4.646 1.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1 0 .708l-6 6a.5.5 0 0 1-.708-.708L10.293 8 4.646 2.354a.5.5 0 0 1 0-.708z"/></svg>',
        ],
        autoplayTimeout: 5000,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1,
                nav: 0,
                dots: 0,
            },
            578: {
                items: 2,
                nav: 0,
                dots: 0,
            }
        },
        onInitialized: function () {
            $(".slide-banner-ads-product .owl-nav .owl-next").attr('role', 'button').attr('aria-label', "next");
            $(".slide-banner-ads-product .owl-nav .owl-prev").attr('role', 'button').attr('aria-label', "prev");
            $('.slide-banner-ads-product .owl-item:not(.active) .item img').attr('loading', 'lazy');
            $(".slide-banner-ads-product .disabled").remove();
        }
    });
    //slide

    let menuProduct = $('.menu-product .icon-down-menu');

    menuProduct.on('click', function () {
        let menuchild = $(this).next();
        menuchild.slideToggle();
        $(this).toggleClass('active');

    })


    var dataSrcValue = $('.video-popup iframe').data('src');
    //var dataSrcValue1 = $('.video-popup video source').data('src');
    var videoUpload = document.getElementById("video-upload");
    $('.video-detail-product').on('click', function () {
        $('.video-popup iframe').attr("src", dataSrcValue);
        $('.video-popup').addClass('active');
        $('.btn-close-video').addClass('active');
    });

    $('.btn-close-video').on('click', function () {
        if ($('.video-popup').hasClass('video')) {
            videoUpload.pause();
            videoUpload.currentTime = 0;
        } else {
            $('.video-popup iframe').attr("src", "");
        }
        $('.video-popup').removeClass('active');
        $('.btn-close-video').removeClass('active');

    });

    let showContent = $('.show-detail');

    showContent.on('click', function () {
        $(this).parent().addClass('active');
        $('.overlay').addClass('show');
    })

    $('.overlay, .icon-exit').on('click', function () {
        $('.specifications, .characteristic').removeClass('active');
        $('.overlay').removeClass('show');
        $('.specifications, .characteristic').scrollTop(0);
    })

    let iconShowFillter = $('.fillter-icon');
    let overlayFiller = $('.overlay-fillter');
    iconShowFillter.on('click', function () {
        iconShowFillter.hide();
        let fillter = $('.product-left');
        fillter.addClass('active');
        overlayFiller.addClass('active');
        $('body').addClass('no-scroll');
    })

    overlayFiller.on('click', function () {
        iconShowFillter.show();
        let fillter = $('.product-left');
        fillter.removeClass('active');
        overlayFiller.removeClass('active');
        $('body').removeClass('no-scroll');
    })

    let moduleProductItem = $('.menu-product .nav-item');
    let btnMoreModuleProduct = $('.more-module-product');
    btnMoreModuleProduct.on('click', function () {
        btnMoreModuleProduct.hide();
        if(moduleProductItem.hasClass('hide'))
        {
            moduleProductItem.removeClass('hide');
        }
    })
})

function toggleMenuMb(eIcon, eSubmenu) {
    eIcon.on('click', function () {
        eIcon.not($(this)).removeClass('active');
        $(this).toggleClass('active');
        eSubmenu.not($(this).next()).slideUp();
        $(this).next().slideToggle();
    })
}




