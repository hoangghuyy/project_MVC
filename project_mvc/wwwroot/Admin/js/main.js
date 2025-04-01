function initAjaxLoad(urlListsLoad, container) {
    let imageLoading = "Đang tải dữ liệu...";
    $(container).html(imageLoading);
    $.post(urlListsLoad, function (data) {
        $(container).html(data);
    }).fail(function () {
        $(container).html("Tải dữ liệu thất bại...");
    });
}
function loadAjaxForm(urlListsLoad, container) {
    debugger
    $.post(urlListsLoad, function (data) {
        $(container).html(data);
    }).fail(function () {
        $(container).html("Tải dữ liệu thất bại...");
    });
}

function showMessage(mess, status) {
    Toastify({
        text: mess,
        duration: 3000,
        //close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        style: {
            background: status === "error" ? "red" : "green" //toán tử 3 ngôi
        },
    }).showToast();
}
//phân trangurlListItem
function paging() {
    $('.pagingData').on('click', function (e) {
        e.preventDefault();
        let p = $(this).attr('href');
        debugger

        $.post(urlListItem + "?page=" + p, function (data) {
            $('#loadGridView').html(data);
            setTimeout(
                function () {
                    paging();
                }, 300);
        }).fail(function () {
            $('#loadGridView').html("Tải dữ liệu thất bại...");
        });
    })


}
//replace giá trị param
function replaceUrlParam(url, paramName, paramValue) {
    if (paramValue == null) {
        paramValue = "";
    }
    if (paramName == null) {
        return url;
    }
    var pattern = new RegExp("\\b(" + paramName + "=).*?(&|$)");
    if (url.search(pattern) >= 0) {
        return url.replace(pattern, "$1" + paramValue + "$2");
    }
    url = url.replace(/\#$/, "");
    return (
        url + (url.indexOf("#") > 0 ? "&" : "#") + paramName + "=" + paramValue
    );
} //replace giá trị param


//load CKEditor start
config = {};
function LoadCKEDITOR(n, o, h) {
    var i = CKEDITOR.instances[n];
    i && i.destroy(!0), CKEditorConfig(n, o, h);
}

function CKEditorConfig(instanceName, fullEditor, height) {
    config.language = 'en';
    //config.extraPlugins = 'youtube';
    config.allowedContent = true;
    config.filebrowserBrowseUrl = '/lib/tinymce/index.html?integration=ckeditor&typeview=3';
    config.filebrowserImageBrowseUrl = '/lib/tinymce/index.html?integration=ckeditor&typeview=3';
    config.filebrowserFlashBrowseUrl = '/lib/tinymce/index.html?integration=ckeditor&typeview=3';
    config.filebrowserUploadUrl = '/lib/tinymce/index.html?integration=ckeditor&typeview=3';
    config.filebrowserImageUploadUrl = '/api/TinyMce/UploadImage';
    config.filebrowserFlashUploadUrl = '/lib/tinymce/index.html?integration=ckeditor&typeview=3';
    config.entities_latin = false;
    config.image2_alignClasses = ['image-left', 'image-center', 'image-right'];
    config.image2_captionedClass = 'image-captioned';

    if (fullEditor) {
        config.toolbarGroups = [
            { name: 'document', groups: ['mode', 'document', 'doctools'] },
            { name: 'clipboard', groups: ['clipboard', 'undo'] },
            //{ name: 'forms', groups: ['forms'] },
            { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
            { name: 'links', groups: ['links'] },
            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
            { name: 'insert', groups: ['insert'] },
            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
            { name: 'styles', groups: ['styles'] },
            { name: 'colors', groups: ['colors'] },
            { name: 'tools', groups: ['tools'] },
            '/',
            { name: 'others', groups: ['others'] }
        ];
        config.extraPlugins = 'image2,youtube,html5audio,html5video,widget,widgetselection,clipboard,lineutils,removeformat';
        //config.removeDialogTabs = 'image:advanced;link:advanced';
        config.removeButtons = 'Radio,TextField,Textarea,Select,Button,HiddenField,ImageButton,Language,PageBreak,CreateDiv,Anchor,Outdent,Indent,Replace,SelectAll,Scayt,Checkbox,Find';
        config.skin = 'office2013';
        config.height = height;
        CKEDITOR.replace(instanceName, config);
    } else {
        config.toolbarGroups = [
            { name: 'document', groups: ['mode', 'document', 'doctools'] },
            { name: 'clipboard', groups: ['clipboard', 'undo'] },
            { name: 'forms', groups: ['forms'] },
            { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
            { name: 'links', groups: ['links'] },
            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
            { name: 'insert', groups: ['insert'] },
            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
            { name: 'styles', groups: ['styles'] },
            { name: 'colors', groups: ['colors'] },
            { name: 'tools', groups: ['tools'] },
            { name: 'about', groups: ['about'] },
            '/',
            { name: 'others', groups: ['others'] }
        ];
        config.removeButtons = 'Radio,TextField,Textarea,Select,Button,HiddenField,ImageButton,Language,PageBreak,CreateDiv,Anchor,Outdent,Indent,Replace,SelectAll,Scayt,Checkbox,Find,Save,NewPage,Preview,Print,Templates,Subscript,Superscript,CopyFormatting,Image,Flash,Table,HorizontalRule,Smiley,SpecialChar,Iframe,NumberedList,BulletedList,Blockquote,BidiLtr,ShowBlocks,About,Cut,Undo,Redo,Copy,Paste,PasteText,Styles,Font,BGColor,PasteFromWord,BidiRtl';
        CKEDITOR.replace(instanceName, config);
    }
}

function updateEditor() { for (var n in CKEDITOR.instances) CKEDITOR.instances[n].updateElement(); }
//load CKEditor end

function SelectFileTyniMce(id, name, type) {
    debugger
    var share_link = "/lib/tinymce/index.html?integration=folderadmin&view=" + id + "&name=" + name + "&typeview=" + type;
    popupwindow(share_link, "Manager file", 1600, 800);
}
function popupwindow(url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}

function ChangeUrlTinyMce(thisNow, id, name, type) {
    var url = thisNow.val();
    var file = {
        'fullPath': url
    };
    if (type == 2) {
        FileTinyMce(file, id, name);
    }
    else {
        UpdatePictureTinyMce(file, id, name);
    }
}
function UpdatePictureTinyMce(file, id, name) {
    var html = '<table class="removeParent">' +
        '<tr>' +
        '<td><img style="width: 100px;" src="' + file.fullPath + '">' +
        '<input type="hidden" name="' + name + '" value="' + file.fullPath + '"><a href="javascript:void(0);" class="removeObject"><i class="fa fa-trash"></i></a></td>' +
        '</tr></table>';
    $("#" + id).html(html);
    $("#" + id).parents(".changeUrlTinyMceParent").find(".changeUrlTinyMce").val(file.fullPath);
    console.log(file);
    removeObject();
    tooltip();
    //if (file.type == "image") {
    //    var url = "/admincms/Image/ConvertReSize?path=" + file.path + "&name=" + file.name;
    //    $.ajax({
    //        url: encodeURI(url), cache: false, type: "Post",
    //        success: function (data) {
    //            console.log(data);
    //        },
    //        error: function (data) {
    //            console.log(data);
    //        }
    //    });
    //}
}
function removeObject() {
    $(".removeObject").click(function () {
        var thisRemove = $(this);
        swal({ title: "", text: "Bạn chắc chắn muốn xóa?", type: "warning", showCancelButton: true, confirmButtonClass: "btn-danger", confirmButtonText: "OK", closeOnConfirm: false },
            function (isConfirm) {
                if (isConfirm) {
                    thisRemove.parents(".removeParent").find('input[type=hidden]').val('');
                    thisRemove.parents(".removeParent").remove();
                }
                swal.close();
            });
    });
} // xóa trong thẻ div

function tooltip() {
    /* CONFIG */
    xOffset = 10;
    yOffset = 20;
    // these 2 variable determine popup's distance from the cursor
    // you might want to adjust to get the right result		
    /* END CONFIG */
    $(".tooltipImage").hover(function (e) {
        this.t = this.src;
        var size = this.fileSize;
        var filename = this.src.replace(/^.*[\\\/]/, '');
        $("body").append("<p id='tooltip'> <span class='tooltipimg'> <img src='" + this.t + "'/> </span>" +
            " <span><b>File Name:" + filename + "</b></span>" +
            " <span><b>Dimensions: " + this.naturalWidth + " x " + this.naturalHeight + "</b></span></p>");
        $("#tooltip")
            .css("top", (e.pageY - xOffset) + "px")
            .css("left", (e.pageX + yOffset) + "px")
            .fadeIn("fast");
    },
        function () {
            this.title = this.t;
            $("#tooltip").remove();
        });
    $("a.tooltip").mousemove(function (e) {
        $("#tooltip")
            .css("top", (e.pageY - xOffset) + "px")
            .css("left", (e.pageX + yOffset) + "px");
    });
};
//load File start
//0: là load ảnh, 1: load album
function FileTinyMce(file, id, name) {
    $("#" + id).val(file.fullPath);
}

function AlbumTinyMce(file, id, name) {
    var html = `<table class="table removeParent">`;
    html += `<tr><td rowspan="2" style="width:50px;"><img class="tooltipImage" style="width: 50px; height: 50px;" src="[AlbumUrl]"><a href="javascript:void(0);" class="removeObject"><i class="fa fa-trash"></i></a></td><td style="width:100px;text-align:left;"><b>Tiêu đề</b></td><td><input class="form-control" type="text" name="AlbumTitle" placeholder="[AlbumTitle]" value=""></td></tr>`;
    html += `<tr><td style="text-align:left;"><b>Link</b></td><td><input type="text" class="form-control" name="AlbumUrl" value="[AlbumUrl]"></td></tr>`;
    html += `<tr><td><input type="text" style="text-align:center;width:70px;display: inline-block;" class="form-control" name="AlbumOrderDisplay" value="0" placeholder="Thứ tự"></td><td style="text-align:left;"><b>Loại</b></td><td>` +
        `<select name="AlbumType" class="form-control" style="width:200px;display:inline-block;">` +
        // `<option value="4">Slide thư viện</option>` +
        `<option value="0">Ảnh/Background</option>` +
        // `<option value="1">Icon/Ảnh nhỏ</option>` +
        //`<option value="2">Icon trang chủ</option>` +
        `<option value="3">Banner</option>` +
        /*`<option value="4">Slide thư viện</option>` +*/
        //`<option value="5">Banner ngoài danh mục cha</option>` +
        //`<option value="6">Popup</option>` +
        `</select>
<input type="radio" class="IsAvatar" name="IsAvatar" value="true" style="margin:0 5px;" data-url="[AlbumUrl]" />Ảnh đại diện
</td></tr></table>`;
    html = html.replaceAll("[AlbumUrl]", file.fullPath);
    html = html.replaceAll("[AlbumTitle]", file.fullPath);
    $("#" + id).prepend(html);
    removeObject();
    $('.IsAvatar').change(function () {
        var src = $(this).data('url');
        $('#Avatar').val(src);
        $('#AddAvatar').find('input').val(src);
        $('#AddAvatar').find('img').attr('src', src);
    });
    tooltip();
    //if (file.type == "image") {
    //    var url = "/admincms/Image/ConvertReSize?path=" + file.path + "&name=" + file.name;
    //    $.ajax({
    //        url: encodeURI(url), cache: false, type: "Post",
    //        success: function (data) {
    //            console.log(data);
    //        },
    //        error: function (data) {
    //            console.log(data);
    //        }
    //    });
    //}
}

function AlbumModuleProductTinyMce(file, id, name) {
    let html = `
            <table class="table removeParent">
                <tr>
                    <td rowspan="3" style="width:50px;">
                        <img style="width: 50px; height: 50px;" src="${file.fullPath}">
                        <a href="javascript:void(0);" class="removeObject">
                            <i class="fa fa-trash"></i>
                        </a>
                    </td>
                    <td style="width:100px;text-align:left;"><span>Tiêu đề</span></td>
                    <td><input class="form-control form-control-sm title" type="text" name="AlbumTitle" value=""></td>
                </tr>
                <tr>
                    <td style="text-align:left;"><span>Link redirect</span></td>
                    <td>
                        <div class="input-group">
                            <input type="text" class="form-control form-control-sm link" name="AlbumAlt" value="">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;"><span>Link ảnh</span></td>
                    <td>
                        <div class="input-group">
                            <input type="text" class="form-control form-control-sm title" name="AlbumUrl" value="${file.fullPath}">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td><input type="text" style="text-align:center;width:70px;display: inline-block;" class="form-control form-control-sm" name="AlbumOrderDisplay" value="0" placeholder="Thứ tự"></td>
                    <td style="text-align:left;"><span>Loại</span></td>
                    <td style="padding: 0 5px">
                        <select name='AlbumType' class="form-control form-control-sm">
                            <option value='0' @(item.AlbumType == 0 ? "selected" : "")>Ảnh/Background</option>
                            <option value='3' @(item.AlbumType == 3 ? "selected" : "")>Banner @Temp.SizeImage("Banner")</option>
                            <option value='4' @(item.AlbumType == 4 ? "selected" : "")>Banner nhỏ danh mục sản phẩm</option>
                        </select>
                        <input type="radio" class="IsAvatar" name="IsAvatar" value="true" style="margin:0 5px;" data-url="${file.fullPath}" />Ảnh đại diện
                    </td>
                </tr>
            </table>
    `;
    $("#" + id).prepend(html);
    removeObject();
    $('.IsAvatar').change(function () {
        var src = $(this).data('url');
        $('#Avatar').val(src);
        $('#AddAvatar').find('input').val(src);
        $('#AddAvatar').find('img').attr('src', src);
    });
    tooltip();
    //if (file.type == "image") {
    //    var url = "/admincms/Image/ConvertReSize?path=" + file.path + "&name=" + file.name;
    //    $.ajax({
    //        url: encodeURI(url), cache: false, type: "Post",
    //        success: function (data) {
    //            console.log(data);
    //        },
    //        error: function (data) {
    //            console.log(data);
    //        }
    //    });
    //}
}

//checkbox
function loadFunctionInit() {
    $("#checkAll").on("click", function () {
        debugger;
        if ($(this).prop("checked")) $(".check").prop("checked", true);
        else $(".check").prop("checked", false);
    });

    $(".check").on("click", function () {
        let isCheck = $(this).prop("checked");
        if (isCheck) {
            if (!$("#checkAll").prop("checked"))
                // false
                $("#checkAll").prop("checked", true);
        } else {
            let ischeckAll = false;
            $(".check").each(function () {
                if (!ischeckAll && $(this).prop("checked")) {
                    ischeckAll = true;
                }
            });
            if (ischeckAll) {
                if (!$("#checkAll").prop("checked"))
                    // false
                    $("#checkAll").prop("checked", true);
            } else {
                $("#checkAll").prop("checked", false);
            }
        }
    });
}

function replaceCommaFirstEnd(str) {
    if (str) {
        if (str.startsWith(",")) {
            str = str.substring(1);
        }
        if (str.endsWith(",")) {
            str = str.substring(0, str.length - 1);
        }
        return str;
    }
}

function containsObject(obj, list) {
    var i;
    for (i = 0; i < list.length; i++) {
        if (list[i] == obj) {
            return true;
        }
    }
    return false;
}

function removeElement(array, elem) {
    var index = array.indexOf(elem);
    if (index > -1) {
        array.splice(index, 1);
    }
}

function RemoveUnicode(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_|\||–|”|“|`/g, "-");
    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1- 
    str = str.replace(/^\-+|\-+$/g, "");
    return str;
} // chuyển name tới NameAscii

function removeAllAlbum(id) {
    $('#' + id).find('.removeParent').remove();
}//xóa album
