var lang = Cookies.get('lang');
if (lang == null || lang == undefined) lang = 'vi';

function ProcessData() {
    //#region Submit
    this.SendContact = function () {
        let $formContact = $("#SendContact");
        $formContact.validate({
            rules: {
                FullName: { required: true },
                Phone: { required: true, minlength: 10, maxlength: 12 },
                Email: { required: true, email: true }
            },
            messages: {
                FullName: { required: "Vui lòng nhập họ tên" },
                Phone: { required: "Vui lòng nhập số điện thoại", minlength: "Từ 10 đến 12 ký tự", maxlength: "Từ 10 đến 12 ký tự" },
                Email: { required: "Vui lòng nhập email", email: "email không chính xác" }
            },
            submitHandler: function () {

                let d = $formContact.serialize();
                $(".btnSendContact").prop("disabled", true).hide();
                $(".load").addClass('show');
                $("body").addClass('disable');
                let action = $formContact.attr('action');
                $.post(action, d, function (data) {
                    if (data.errors) {
                        $(".btnSendContact").prop("disabled", false).show();
                        $(".load").removeClass('show');
                        $("body").removeClass('disable');
                        OpenAlert(data.message, false);
                        console.log(data.logs);
                    } else {
                        OpenAlert(data.message, true);
                        setInterval(function () { window.location.reload(); }, 3000);
                        $(".load").removeClass('show');
                        $("body").removeClass('disable');
                        $(".btnSendContact").show();
                    }
                }).fail(function () {
                    $(".btnSendContact").prop("disabled", false).show();
                    $(".load").removeClass('show');
                    $("body").removeClass('disable');
                    OpenAlert("Gửi thất bại", false);
                });
                return false;
            }
        });
    };
};
//#region Utility
//function loadAjax(urlContent, container) {
//    // $(container).append(TemplateLoading);
//    // $("html,body").animate({ scrollTop: $(container).offset().top - 200 }, 1e3);
//    $.ajax({
//        url: encodeURI(urlContent),
//        cache: false,
//        type: "POST",
//        success: function (data) {
//            $(container).html(data);
//            $(container).children('.list-search').find('.hidden').removeClass('hidden').fadeIn();
//        }
//    });
//}
function CheckLink(n, i) { return "" !== i && null != i ? i : "" == n || null != i && "" != i ? "javascript:void(0)" : "/" + n }
function formatDate(t) { if (null == t) return ""; t = new Date(t); return t.getDate() + "/" + (t.getMonth() + 1) + "/" + t.getFullYear() }
function formatFrice(n, e) { return null == n ? "Liên hệ" : n.toFixed(0).replace(/./g, function (n, e, r) { return 0 < e && "." !== n && (r.length - e) % 3 == 0 ? "," + n : n }) + e }
//#endregion
$(function () {
    var e = $("#ReceiverEmailFrm");
    $("#btnReceiverEmail").click(function () {
        $("#btnReceiverEmail").attr("disabled", !0), e.valid() ? e.submit() : $("#btnReceiverEmail").attr("disabled", !1);
        console.log("0")
    }),
        e.validate({
            rules: { Email: { required: true, email: true }, },
            messages: { Email: { required: "Vui lòng nhập email", email: "email không chính xác" }, },
            submitHandler: function () {
                $.post("/Ajax/Home/ReceiverEmail", e.serialize(), function (e) {
                    e.errors
                        ? OpenAlert(e.message, !1)
                        : (OpenAlert(e.message, !0),
                            setTimeout(function () {
                                null != e.url ? (window.location.href = e.url) : window.location.reload();
                            }, 2e3));
                });
            },
        });
    $('.menu-first li a').each(function () {
        var link = window.location.origin + window.location.pathname;
        if (this.href.trim() === link)
            $(this).addClass('active');
    });
    $('.drop-down li a').each(function () {
        var link = window.location.origin + window.location.pathname;
        if (this.href.trim() === link)
            $(this).parents('.drop-down li a').addClass('active');
    });

    $('input.valid').on('keyup', function (e) {
        e.preventDefault();
        var old = $(this).val();
        old = old.replace(/!|\$|\\|\{|\}|\||%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\:|\;|\'|\"|\&|\#|\[|\]|~|$|”|“|`/g, "");
        old = old.replace(/-+-/g, "");
        $(this).val(old);
    });
    $('textarea.valid').on('keyup', function (e) {
        e.preventDefault();
        var old = $(this).val();
        old = old.replace(/!|\$|\\|\{|\}|\||@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\:|\;|\'|\"|\&|\#|\[|\]|~|$|”|“|`/g, "");
        old = old.replace(/-+-/g, "");
        $(this).val(old);
    });
    $('textarea.valid-comment').on('keyup', function (e) {
        e.preventDefault();
        var old = $(this).val();
        old = old.replace(/!|\$|\\|\{|\}|\||@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\:|\;|\'|\"|\&|\#|\[|\]|~|$|”|“|`/g, "");
        old = old.replace(/-+-/g, "");
        $(this).val(old);
    });
    $('input.email').on('keyup', function (e) {
        e.preventDefault();
        var old = $(this).val();
        old = old.replace(/!|\$|\\|\{|\}|\||%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\:|\;|\'|\"|\&|\#|\[|\]|~|$|”|“|`/g, "");
        old = old.replace(/-+-/g, "");
        $(this).val(old);
    });
    $('input.number').on('keyup', function () {
        var old = $(this).val();
        old = old.replace(/\D/g, '');
        $(this).val(old);
    })
    $('input.fullname').on('keyup', function (e) {
        e.preventDefault();
        var old = $(this).val();
        old = old.replace(/!|\$|\\|\{|\}|\||@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\:|\;|\'|\"|\&|\#|\[|\]|~|$|_|–|”|“|`/g, "");
        old = old.replace(/-+-/g, "");
        old = old.replace(/0|1|2|3|4|5|6|7|8|9/g, '');
        old = old.replace(/^\-+|\-+$/g, "");
        $(this).val(old);
    });
    $('input.link').on('keyup', function (e) {
        e.preventDefault();
        var old = $(this).val();
        var newval = removeTagHTML(old);
        newval = newval.replace(/!|\$|\\|\{|\@|\}|\^|\*|\(|\)|\+|\<|\>|,|\;|\'|\"|\[|\]|~|$|”|“|`/g, "");
        $(this).val(newval);
    });
});

//#region Call function
//var process = new ProcessData();
//$(function () {

//    //$('.lang-vi').click(function (e) {
//    //    e.preventDefault();
//    //    if ('vi' === lang) return;
//    //    else {
//    //        Cookies.set('lang', 'vi', { expires: 1 });
//    //        window.location.href = "/";
//    //    }
//    //});
//    //$('.lang-en').click(function (e) {
//    //    e.preventDefault();
//    //    if ('en' === lang) return;
//    //    else {
//    //        Cookies.set('lang', 'en', { expires: 1 });
//    //        window.location.href = "/";
//    //    }
//    //});
//    //$('.lang-ko').click(function (e) {
//    //    e.preventDefault();
//    //    if ('ko' === lang) return;
//    //    else {
//    //        Cookies.set('lang', 'ko', { expires: 1 });
//    //        window.location.href = "/";
//    //    }
//    //});
//    //var ck = Cookies.get('lang');
//    //if (ck == undefined)
//    //    ck = 'vi';
//    //var current = $('html').attr('lang');
//    //if (ck != current) {
//    //    window.location.reload();
//    //}
//});
//#region hidden
/*async function LoadResource() { var o = "/DataJson/Resource/Resources_" + lang + ".json"; await $.ajax({ url: o, dataType: "json", async: 1, success: function (o) { Resource = o }, error: function (o) { console.log("Dữ liệu không tồn tại") } }) }*/
/*function GetSource(e) { var r = Resource[e]; return null != r ? r : "[" + e + "]" }*/
var getUrlParameter = function (r) { for (var t, e = window.location.search.substring(1).split("&"), n = 0; n < e.length; n++)if ((t = e[n].split("="))[0] === r) return t[1], decodeURIComponent(t[1]); return !1 };
function RemoveUnicode(e) { return e = (e = (e = (e = (e = (e = (e = (e = (e = (e = (e = (e = e.toLowerCase()).replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a")).replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e")).replace(/ì|í|ị|ỉ|ĩ/g, "i")).replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o")).replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u")).replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y")).replace(/đ/g, "d")).replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_|–|”|“|`/g, "-")).replace(/\s+/g, "-")).replace(/-+-/g, "-")).replace(/^\-+|\-+$/g, "") }
function removeTagHTML(str) {
    if ((str === null) || (str === ''))
        return '';
    else
        str = str.toString();
    return str.replace(/(<([^>]+)>)/ig, '');
}
function resizeImage(e, s) {
    s = parseInt($(e).width()) * s;
    $(e).css({ height: s + "px" });
}

function getValueFormMutilSelect(t) { var e, i = ""; return $(t).find("input[type='checkbox']:checked, input[type='radio']:checked, input[type='text'],input[type='number'], input[type='hidden'], select").each(function () { e = $(this).attr("name"), "" != $(this).val() && (i += "&" + e + "=" + $(this).val()) }), "" != i && (i = i.substring(1)), i }
function getparram(t) {
    var n = new Object;
    return $(t).find("input[type='checkbox']:checked, input[type='radio']:checked, input[type='text'],input[type='number'], input[type='hidden'], select").each(function() {
        var t = $(this).attr("name"),
            e = $(this).val();
        "" != $(this).val() && "" != t && null != t && (n[t] = e)
    }), n
}
function formatPrice(n, e) { return null == n ? "Liên hệ" : n.toFixed(0).replace(/./g, function (n, e, r) { return 0 < e && "." !== n && (r.length - e) % 3 == 0 ? "," + n : n }) + e }
function AlertError(r) { window.alert(r) }
function OpenAlert(msg, success) {
    $('.alrt-popup .main').html(msg);
    success == true ? $('.alrt-popup').addClass('success') : $('.alrt-popup').removeClass('success');
    $('.alrt-popup,.overlay').addClass('show');
    $('.close-alrt').click(function () {
        CloseAlert();
    });
}
function CloseAlert() {
    $('.alrt-popup').removeClass('success');
    $('.alrt-popup .main').html('');
    $('.alrt-popup,.overlay').removeClass('show');
}
//#endregion
