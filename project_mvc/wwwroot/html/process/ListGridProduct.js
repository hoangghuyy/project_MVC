$(() => {
    let sort = getUrlParameter('sort');
    let trademark = getUrlParameter('hsx');
    let p = getUrlParameter('page');
    viewMore();
    let sortSelectE = $('#sort-product select');
    let trademarkE = $('.list-trademark li');
    sortSelectE.on('change', function () {
        let val = $(this).val();
        if (val) {
            hanlderFillterProduct(trademark, val, 1);
        }
    })

    trademarkE.on('click', function() {
        trademarkE.not($(this)).removeClass('active');
        if(!$(this).hasClass('active')) {
            let val = $(this).data('id');
            $(this).addClass('active');
            if (val) {
                hanlderFillterProduct(val, sort, 1);
            }
        } else {
            $(this).removeClass('active');
            hanlderFillterProduct('', sort, 1);
        }
    })


    $('.select-attr input[type=checkbox]').change(function () {
        let name = $(this).data('name');
        let val = [];
        $('input[data-name=' + name + ']:checked').each(function () {
            val.push($(this).val());
        });
        $('input[name=' + name + ']').val(val.toString());
        hanlderFillterProduct(trademark, sort, p);
    });
})

function viewMore() {
    $(".page div").click(function (n) {
        n.preventDefault();
        let sort = getUrlParameter('sort');
        let trademark = getUrlParameter('trademark');
        let p = $(this).data("page");
        $("#page").val(p);
        hanlderFillterProduct(trademark, sort, p);
    })
}

function hanlderFillterProduct(trademark = '', sort = 0, p = 0) {
    let hastag = getHastag(trademark, sort);
    attr = GetAttr();
    let seo = $('#seoUrl').val();
    let container = '#load-product';
    let url = '/Ajax/Content/PartialFillterProduct?seoUrl=' + seo + '&page=' + p;
    if (hastag) url += hastag
    if (attr) url += "&" + attr;
    $(".load").addClass('show');
    $.ajax({
        url: encodeURI(url),
        cache: false,
        type: "POST",
        dataType: 'html',
        success: function (data) {
            $(".load").removeClass('show');
            $(container).html(data);
            RenewUrl(trademark, seo, sort, p);
            viewMore();
            getTotal();
        },
        errors: function () {
            window.alert("Tải dữ liệu không thành công");
        }
    });
}

function RenewUrl(trademark, seo, sort, p) {
    let attr = getparram('.select-attr');
    let newUrl = '';
    let hastag = '';
    let i = 1, j = 1;
    for (let key in attr) {
        if (attr[key] != 0) {
            if (i == 1) hastag += key + '=' + attr[key];
            else hastag += '&' + key + '=' + attr[key];
            i++;
        }
    }
    if (seo.startsWith("/")) {
        newUrl = seo;
    }
    else {
        newUrl = '/' + seo;
    }
    if (!p || parseInt(p) === 1) {
        if (sort) {
            newUrl += (j == 1 ? '?' : '&') + 'sort=' + sort;
            j++;
        }
        if(trademark) 
        {
            newUrl += (j == 1 ? '?' : '&') + 'hsx=' + trademark;
            j++;
        }
        if (hastag) {
            newUrl += (j == 1 ? '?' : '&') + hastag;
            j++;
        }
    } else {
        if (p) {
            newUrl += (j == 1 ? '?' : '&') + 'page=' + p;
            j++;
        }
        if (sort) { newUrl += (j == 1 ? '?' : '&') + 'sort=' + sort; j++; }
        if(trademark) 
        {
            newUrl += (j == 1 ? '?' : '&') + 'hsx=' + trademark;
            j++;
        }
        if (hastag) { newUrl += (j == 1 ? '?' : '&') + hastag; j++; }
    }
    window.history.pushState('', '', newUrl);
}

function GetAttr() {
    var attr = getparram('.select-attr');
    var hastag = '';
    var i = 1;
    for (var key in attr) {
        if (attr[key] != 0) {
            if (i == 1) hastag += key + '=' + attr[key];
            else hastag += '&' + key + '=' + attr[key];
            i++;
        }
    }
    return hastag;
}


function getHastag(trademark, sort) {
    let hastag = '';

    if (sort) {
        hastag += "&sort=" + sort;
    }
    if(trademark){
        hastag += "&hsx=" + trademark;
    }
    return hastag;
}