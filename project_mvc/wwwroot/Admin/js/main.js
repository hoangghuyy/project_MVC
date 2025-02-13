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
//phân trang
function paging() {
    $(".pagingData").each(function () {
        var a = $(this).attr("href"),
            e = a.replaceAll("#page=", "");
        a = replaceUrlParam(window.location.href, "page", e), $(this).attr("href", a);
    });
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

//bt-search
function searchItem() {
    const searchE = $("#btnSearch");
    searchE.on("click", function () {
        debugger
        var form = $("#searchFrm");
        window.location.href = '#' + getValueFormMutilSelect(form);
        return false;
    });
}

function getValueFormMutilSelect(form) {
    var arrParam = '';
    var idMselect;
    $(form).find("input[type='checkbox']:checked, input[type='radio']:checked, input[type='text'],input[type='number'], input[type='hidden'], select").each(function () {
        idMselect = $(this).attr("name");
        if ($(this).val() != '' && $(this).val() != '')
            arrParam += "&" + idMselect + "=" + $(this).val();
    });
    if (arrParam != '')
        arrParam = arrParam.substring(1);
    return arrParam;
}

//AjaxForm 
function handleAction() {
    const addE = $('.btn-add');
    const editE = $('.btn-edit');
    const deleteE = $('.btn-delete');
    const deleteAllE = $('.deleteAll');
    const backE = $('.btn-back');
    if (addE) {
        addE.on("click", function () {
            loadAjaxForm(urlForm, "#AjaxForm");
            $('#loadGridView').html('');
            //$('#AjaxForm').show();
        });
    }
    if (editE) {
        editE.on("click", function () {
            const id = $(this).data("id");
            loadAjaxForm(urlForm + "?id=" + id, "#AjaxForm");
            $('#loadGridView').html('');
            $('#AjaxForm').show();
        });
    }
    if (backE) {
        backE.on("click", function () {
            loadAjaxForm(urlListItem, "#loadGridView");
            //$('#loadGridView').show();
            $('#AjaxForm').html('');
        });
    }
    if (deleteE) {
        deleteE.on("click", function () {
            const id = $(this).data("id");

            $.confirm({
                content: 'Bạn có chắc muốn xóa không?',
                buttons: {
                    confirm: {
                        text: 'Xóa',
                        btnClass: 'btn-red',
                        action: function () {
                            $.ajax({
                                url: urlAction,
                                type: "POST",
                                contentType: "application/x-www-form-urlencoded",
                                data: { action: "Delete", Id: id },
                                success: function (data) {
                                    loadAjaxForm(urlListItem, "#loadGridView");
                                    showMessage(data.message, "success");
                                },
                                error: function () {
                                    showMessage(data.message, "error");
                                }
                            });
                        }
                    },
                    cancel: {
                        text: 'Hủy',
                        action: function () { }
                    }
                }
            });
        });
    }
    if (deleteAllE) {
        deleteAllE.on("click", function () {
            //e.preventDefault();
            var arrRowId = '';
            $("input.check[type='checkbox']:checked").not("#checkAll").not(".checkAll").each(function () {
                arrRowId += $(this).val() + ",";
            });
            arrRowId = (arrRowId.length > 0) ? arrRowId.substring(0, arrRowId.length - 1) : arrRowId;
            if (arrRowId != "") {
                $.confirm({
                    title: false,
                    content: 'Bạn có chắc muốn xóa các mục đã chọn không?',
                    buttons: {
                        confirm: {
                            text: 'Xóa',
                            btnClass: 'btn-red',
                            action: function () {
                                $.ajax({
                                    url: urlAction,
                                    type: "POST",
                                    contentType: "application/x-www-form-urlencoded",
                                    data: { action: "DeleteAll", ItemId: arrRowId }, // Gửi dữ liệu qua body
                                    success: function (data) {
                                        loadAjaxForm(urlListItem, "#loadGridView");
                                        showMessage(data.message, "success");
                                    },
                                    error: function () {
                                        showMessage("Xóa thất bại", "error");
                                    }
                                });
                            }
                        },
                        cancel: {
                            text: 'Hủy',
                            action: function () { }
                        }
                    }
                });
            } else {
                showMessage("Hãy chọn ít nhất 1 bài viết", "success");;
            }

            return false;
        });
    }
}


//load CKEditor start
config = {};
function LoadCKEDITOR(n, o, h) {
    var i = CKEDITOR.instances[n];
    i && i.destroy(!0), CKEditorConfig(n, o, h);
}

function CKEditorConfig(instanceName, fullEditor, height) {
    config.language = 'vi';
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
