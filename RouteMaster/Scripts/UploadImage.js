const dropZone = document.getElementById('dropZone');
const imagePreview = document.getElementById('imagePreview');
const fileInput = document.getElementById('files');
const fileLabel = document.getElementById('fileLabel');
const uploadContent = document.getElementById('uploadContent');


dropZone.addEventListener('dragover', (event) => {
    event.preventDefault();
    dropZone.classList.add('drag-over');
});
dropZone.addEventListener('dragleave', () => {
    dropZone.classList.remove('drag-over');
});
dropZone.addEventListener('drop', (event) => {
    event.preventDefault();
    dropZone.classList.remove('drag-over');

    var files = event.dataTransfer.files;
    fileInput.files = files; // 将拖放的文件赋值给文件输入元素

    handleFiles(files);
});

fileInput.addEventListener('change', (event) => {
    var files = event.target.files;

    handleFiles(files);
});
function handleFiles(files) {
    imagePreview.innerHTML = ''; // 清空预览区域
    const filesAry = Object.entries(files);
    let size = 0;

    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        if (file.type.startsWith('image/')) {
            const reader = new FileReader();
            reader.onload = (event) => {
                const imageUrl = event.target.result;
                imagePreview.insertAdjacentHTML('beforeend', `<div class="img-div">
<img src="${imageUrl}" class="preview-image d-block" id="img${i}">
<input type="text" class="form-control d-none" placeholder="為相片做一個註解" name="imgName" id="imgName${i}" style="margin: 10px; width: 150px;">
<input type="button" class="edit-btn btn btn-primary" value="編輯" id="btn-edit${i}" style="margin: 0px 6px 0px 10px;">
<input type="button" class="delete-btn btn btn-danger" value="刪除" id="btn-delete${i}">
</div>`);

                //const imageElement = document.createElement('img');
                //imageElement.src = imageUrl;
                //imageElement.classList.add('preview-image', 'd-block');
                //imageElement.id = `img${i}`;

                //const divElement = document.createElement('div');
                //divElement.classList.add('img-div');

                //const btnElement = document.createElement('input');
                //btnElement.type = "button";
                //btnElement.classList.add('edit-btn', 'btn', 'btn-primary');
                //btnElement.style.margin = '0 6px 0 10px';
                //btnElement.value = "編輯";
                //btnElement.id = `btn-edit${i}`;

                //const deleteBtnElement = document.createElement('input');
                //deleteBtnElement.type = "button";
                //deleteBtnElement.classList.add('delete-btn', 'btn', 'btn-danger');
                //deleteBtnElement.value = "刪除";
                //deleteBtnElement.id = `btn-delete${i}`;

                //const inputElement = document.createElement('input');
                //inputElement.type = "text";
                //inputElement.classList.add('form-control', 'd-none');
                //inputElement.style.margin = "10px";
                //inputElement.style.width = "150px";
                //inputElement.placeholder = "為相片做一個註解";
                //inputElement.name = `imgName`;
                //inputElement.id = `imgName${i}`;



                //imagePreview.appendChild(divElement);
                //divElement.appendChild(imageElement);
                //imageElement.insertAdjacentElement('afterend', deleteBtnElement);
                //imageElement.insertAdjacentElement('afterend', btnElement);
                //imageElement.insertAdjacentElement('afterend', inputElement);


                 //点击编辑按钮时触发的事件处理程序
                $(`#btn-edit${i}`).on('click', function (event) {
                    // 创建文本输入框
                    $(`#imgName${i}`).removeClass("d-none");
                });

                $(`#btn-delete${i}`).on('click', function () {
                    const filesArray = Array.from(fileInput.files); // 将FileList对象转换为数组
                    filesArray.splice(i, 1); // 从数组中删除指定索引的文件
                    const newFileList = new DataTransfer(); // 创建新的DataTransfer对象
                    filesArray.forEach(function (file) {
                        newFileList.items.add(file); // 将更新后的文件添加到DataTransfer对象中
                    });
                    fileInput.files = newFileList.files; // 将新的FileList对象重新赋值给fileInput元素

                    handleFiles(filesArray);
                });
            };

            reader.readAsDataURL(file);
        }
    }
    filesAry.forEach(file => size += file[1].size);

    var TotalSize = +(Math.round((size / 1024000) + "e+2") + "e-2");
    document.querySelector("#uploadContent").textContent = `已上傳的照片大小：${TotalSize} / 4MB`;
    $(".progress-bar-div").removeClass("d-none");
    $(".progress-bar").removeClass("d-none");
    if (TotalSize < 4) {
        document.querySelector("#uploadContent").innerHTML += `<i class="fa-solid fa-check"></i>`;
        document.querySelector("#uploadContent").classList.add('text-primary');
        document.querySelector("#uploadContent").classList.remove('text-danger');
        document.querySelector(".progress-bar").style.width = `${TotalSize / 4 * 100}%`;
        document.querySelector(".progress-bar").classList.remove('bg-danger');
        document.querySelector("#submit-btn").removeAttribute('disabled');
    }
    else {
        document.querySelector("#uploadContent").innerHTML += `<i class="fa-solid fa-xmark"></i>`;
        document.querySelector("#uploadContent").classList.add('text-danger');
        document.querySelector("#uploadContent").classList.remove('text-primary');
        document.querySelector(".progress-bar").style.width = `100%`;
        document.querySelector(".progress-bar").classList.add('bg-danger');
        document.querySelector("#submit-btn").setAttribute('disabled', 'disabled');
    }
}
    //var myCarousel = document.querySelector('#myCarousel');
    //var carousel = new bootstrap.Carousel(myCarousel);


const bigShow = document.querySelector(".layout-wrapper");
bigShow.insertAdjacentHTML('beforeend', carousel);

