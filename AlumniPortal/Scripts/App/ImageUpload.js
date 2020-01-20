$(function () {

    //preview uploaded image
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            var file = input.files[0];
            reader.readAsDataURL(file);
            
            var image = new Image();
            
            reader.onload = function (e) {
                image.src = e.target.result;
                
                image.onload = function() {
                    var w = this.width,
                        h = this.height,
                        s = ~~(file.size / 1024) + 'KB';
                    $('#imgDetails').html("<p>width: " + w + "</p><p>height: " + h + "</p><p>size: " + s + "</p>");

                    if (s > 2000) {
                        alert.show("file size of " + s + " is too large. <br/> Recommended size is under 300 KB");
                    }

                    $('#imgPreview').attr('src', e.target.result);
                    $('#imgPreviewContainer').show();
                    $('#currentImage').hide();
                }
            }
        }
    }

    $("#uploadContainer").on('change', '#ImgInput', function () {
        readURL(this);
    });

    //keep new image; discard old
    $('#keepCurImg').on('click', function () {
        $('#uploadContainer').empty();
        $('#uploadContainer').html('<input id="ImgInput" type="file" name="upload" />');
        $('#imgPreviewContainer').hide();
        $('#currentImage').show();
    });
});
