$(() => {
    Comments();
    ShowReplyComment();
    ReplyCommentAction();
    MoreReplyComment();
    MoreComment();
})
function Comments() {
    let formComment = $("#form-comment");
    //$("#btn-comment").click(function (e) {
    //    e.preventDefault();
    //    formComment.submit();
    //});
    formComment.validate({
        rules: {
            FullName: { required: true },
            Phone: { required: true, minlength: 10, maxlength: 12 },
            Content: { required: true },
            Stars: { required: true }
        },
        messages: {
            FullName: { required: "" },
            Phone: { required: "", minlength: "", maxlength: "" },
            Content: { required: "" },
            Stars: { required: "Vui lòng chọn đánh giá" }
        },
        submitHandler: function () {
            let d = formComment.serialize();
            $("#btn-comment").prop("disabled", true).hide();
            $(".load").addClass('show');
            $("body").addClass('disable');
            formComment.ajaxSubmit({
                success: function (data) {
                    if (data.errors === false) {
                        let image = '';
                        if (data.data.urlPicture) {
                            let listImgs = JSON.parse(data.data.urlPicture);
                            image = `
                            <div class="list-img">
                                ${listImgs && listImgs.map(img => (`
                                    <div class="img" data-src="${img.AlbumUrl}" data-fancybox="comment" data-caption="${data.data.fullname}">
                                        <img src="${img.AlbumUrl}" alt="${data.data.fullname}" />}
                                    </div>
                                `))}
                            </div>
                        `;
                        }
                        let rate = '';
                        for (let i = 1; i <= data.data.rate; i++) {
                            rate += `
                                <div class="icon">
                                    <svg xmlns="http://www.w3.org/2000/svg" height="15"
                                        viewBox="0 0 576 512">
                                        <path d="M316.9 18C311.6 7 300.4 0 288.1 0s-23.4 7-28.8 18L195 150.3 51.4 171.5c-12 1.8-22 10.2-25.7 21.7s-.7 24.2 7.9 32.7L137.8 329 113.2 474.7c-2 12 3 24.2 12.9 31.3s23 8 33.8 2.3l128.3-68.5 128.3 68.5c10.8 5.7 23.9 4.9 33.8-2.3s14.9-19.3 12.9-31.3L438.5 329 542.7 225.9c8.6-8.5 11.7-21.2 7.9-32.7s-13.7-19.9-25.7-21.7L381.2 150.3 316.9 18z" />
                                    </svg>
                                </div>
                            `;
                        }
                        let html = `
                            <div class="item">
                                <div class="left"><img src="/html/style/images/avatar.webp" alt="icon"></div>
                                <div class="right">
                                    <div class="name">
                                        <p>${data.data.fullname}</p>
                                    </div>
                                    <div class="star">
                                        ${rate}
                                    </div>
                                    <div class="paragraph smaller detail-content">
                                        ${data.data.content}
                                    </div>
                                    ${data.data.urlPicture !== null ? image.replaceAll(',', '') : ""}
                                </div>
                            </div>
                        `;
                        var lengthItem;
                        if (data.data.rate != null && data.data.rate > 0) {
                            lengthItem = $('#rate-list').length;
                            if (lengthItem > 0) {
                                if ($('#rate-list .item').length > 0) {
                                    $('#rate-list .item:nth-child(1)').before(html);
                                } else {
                                    $('#rate-list').append(html);
                                }
                            }
                        }
                        $("#btn-comment").prop("disabled", false).show();
                        $(".load").removeClass('show');
                        $("body").removeClass('disable');
                        let totalRate = $('.num-cmt').attr('data-total-comment');
                        $('.num-cmt span').text(`${parseInt(totalRate) + 1}`);
                        $('#form-comment').trigger('reset');
                        $("#AvatarComment").val("");
                        $('.list-feedback').removeClass('hidden')
                        $('.list-img-rate .item').remove();
                        let body = $("html, body");
                        body.stop().animate({ scrollTop: $('.list-feedback').offset().top - 100 }, 500, 'swing', function () {
                        });
                    }
                    else {
                        $("#btn-comment").prop("disabled", false).show();
                        $(".load").removeClass('show');
                        $("body").removeClass('disable');
                        OpenAlert(data.message, false);
                        console.log(data.logs);
                    }
                },
                error: function () {
                    $("#btn-comment").prop("disabled", false).show();
                    $(".load").removeClass('show');
                    $("body").removeClass('disable');
                    OpenAlert(data.message, false);
                    console.log(data.logs);
                }
            })
            return false;
        }
    });
}

function HtmlFormReplyComment(parentid, type, name, container) {
    let cid = $('#form-comment input[name=ContentID]').val();
    var customeid = $('#UserID').val();
    var fullname = $('#form-comment input[name=Fullname]').val();
    var email = $('#form-comment input[name=Email]').val();
    var phone = $('#form-comment input[name=Phone]').val();
    var html = ``;
    $.get("/html/template/comment.html", function (data,) {
        html = data;
        html = html.replace("[ProductID]", cid)
            .replace("[Type]", type)
            .replace("[ParentID]", parentid)
            .replace("[Fullname]", fullname)
            .replace("[Email]", email).replace("[Content]", name);
        html = html.replace("[EnterComment]", "Nhập nội dung");
        html = html.replace("[DinhKemAnh]", "Đính kèm ảnh");
        html = html.replace("[FullName]", "Họ tên");
        html = html.replace("[Send]", "Bình luận");
        html = html.replace("[ParentID]", parentid);
        html = html.replace("[Xoa]", "Xoá");
        $(container).append(html);
    });
}

function ShowReplyComment() {
    $('.show-repply-comment').click(function () {
        var id = $(this).data('id');
        var parentid = $(this).data('pid');
        var name = $(this).data('name');
        $('.list-comment').find('.reply-comment').remove();
        HtmlFormReplyComment(parentid, 'Product', (name != null ? "@" + name + " - " : ''), '.list-comment .item .action.item-' + id);
        showFormComment();
        ReplyCommentAction();
    });
}

function ReplyCommentAction() {
    var formrepplycomment = $("#form-reply-comment");
    $("#btn-reply-comment").click(function () { formrepplycomment.submit(); });
    formrepplycomment.validate({
        rules: {
            FullName: { required: true },
            Email: { required: true, email: true },
            Content: { required: true }
        },
        messages: {
            FullName: { required: "" },
            Email: { required: "", email: "" },
            Content: { required: "" }
        },
        submitHandler: function () {
            let d = formrepplycomment.serialize();
            $("#btn-reply-comment").prop("disabled", true).hide();
            $(".load").addClass('show');
            $("body").addClass('disable');
            formrepplycomment.ajaxSubmit({
                success: function (data) {
                    if (data.errors === false) {
                        let img = `
                            <div class="img-cmt" data-src="${data.data.urlPicture}" data-fancybox="comment" data-caption="${data.data.fullname}">
                                <img src="${data.data.urlPicture}" alt="${data.data.fullname}"/>
                            </div>
                        `;
                        let html = `
                            <div class="item">
                                <div class="info">
                                    <div class="avt">
                                        <i class="fa-solid fa-user"></i>
                                    </div>
                                    <div class="content">
                                        <div class="user-text">${data.data.fullname}</div>
                                        <p>${data.data.content}</p>
                                        ${data.data.urlPicture !== null ? img : ''}
                                    </div>
                                    <div class="action item-${data.data.id}">
                                        <span class="_like"><i class="fa-solid fa-thumbs-up"></i> (0) Thích</span>
                                        <span class="time-span">Vừa xong</span>
                                    </div>
                                </div>
                            </div>
                        `;
                        var lengthItem;
                        lengthItem = $('.list-repply-comment.reply-' + data.data.parentID + ' > .item').length;
                        if (lengthItem > 0)
                            $('.list-repply-comment.reply-' + data.data.parentID + ' > .item:nth-of-type(1)').before(html);
                        else
                            $('.list-repply-comment.reply-' + data.data.parentID + '').html(html);
                        $("#btn-reply-comment").prop("disabled", false).show();
                        $(".load").removeClass('show');
                        $("body").removeClass('disable');
                        $("#AvatarComment").val("");
                        $('.input-file .img-block .img').html('');
                        $('.input-file .img-block').hide();
                        $('.input-file label span').text("Đính kèm ảnh");
                        //ReplyComment();
                    }
                    else {
                        $("#btn-reply-comment").prop("disabled", false).show();
                        $(".load").removeClass('show');
                        $("body").removeClass('disable');
                        OpenAlert(data.message, false);
                        console.log(data.logs);
                    }
                },
                error: function () {
                    $("#btn-reply-comment").prop("disabled", false).show();
                    $(".load").removeClass('show');
                    $("body").removeClass('disable');
                    OpenAlert(data.message, false);
                    console.log(data.logs);
                }
            })
            return false;
        }
    });
}

function MoreReplyComment() {
    $(".page-reply").click(function (e) {
        let productid = $(this).attr('data-product-id');
        let parentid = $(this).attr('data-parentid');
        e.preventDefault();
        $(".load").addClass('show');
        $(this).remove();
        let url = "/Ajax/Comment/AjaxReply?productId=" + productid + "&parentId=" + parentid;
        $.ajax({
            url: encodeURI(url),
            cache: false,
            type: "POST",
            success: function (data) {
                $('.list-repply-comment.reply-' + parentid).append(data);
                $(".load").removeClass('show');
            }
        });
    });
}

function MoreComment() {
    $(".page div").click(function (n) {
        n.preventDefault();
        let productId = $('#productId').val();
        var p = $(this).data("page");
        $(".load").addClass('show');
        $("#page-comment").val(p);
        $.ajax({
            url: encodeURI("/AjaxComment?page=" + p + "&productId=" + productId),
            cache: false,
            type: "POST",
            success: function (data) {
                $('#rate-list').html(data);
                ShowReplyComment();
                ReplyCommentAction();
                MoreReplyComment();
                MoreComment();
                $(".load").removeClass('show');
                var body = $("html, body");
                body.stop().animate({ scrollTop: $('.list-feedback').offset().top - 100 }, 500, 'swing', function () {
                });
            }
        });
    })
}

function LikeComment(id) {
    var like = $('.action.item-' + id + ' ._like').children('span').text().replace("(", "").replace(")", "");
    var n = parseInt(like) + 1;
    $('.action.item-' + id + ' ._like').children('span').text(n);
    var url = "/Ajax/Comment/Good?parentId=" + id;
    $.ajax({
        url: encodeURI(url),
        cache: false,
        type: "POST",
        success: function (data) { }
    });
}