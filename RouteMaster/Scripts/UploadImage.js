const dropZone = document.getElementById('dropZone');
const imagePreview = document.getElementById('imagePreview');
const fileInput = document.getElementById('files');
const fileLabel = document.getElementById('fileLabel');
const uploadContent = document.getElementById('uploadContent');
$(".progress-bar-div").hide();
$(".progress-bar").hide();

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
    const files = event.target.files;

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
                const imageElement = document.createElement('img');
                imageElement.src = imageUrl;
                imageElement.classList.add('preview-image');
                imageElement.id = `img${i}`;

                const btnElement = document.createElement('input');
                btnElement.type = "button";
                btnElement.classList.add('edit','btn','btn-primary');
                btnElement.value = "編輯";
                btnElement.id = `btn-edit${i}`;

                const deleteBtnElement = document.createElement('input');
                console.log(deleteBtnElement);
                deleteBtnElement.type = "button";
                deleteBtnElement.classList.add('delete', 'btn', 'btn-danger');
                deleteBtnElement.value = "刪除";
                deleteBtnElement.id = `btn-delete${i}`;

                imagePreview.appendChild(imageElement);
                imageElement.insertAdjacentElement('afterend', deleteBtnElement);
                imageElement.insertAdjacentElement('afterend', btnElement);
                // 点击编辑按钮时触发的事件处理程序
                $(`#btn-edit${i}`).on('click', function () {
                    // 创建文本输入框
                    const inputElement = document.createElement('input');
                    inputElement.type = "text";
                    parentElement.appendChild(inputElement);
                });
            };

            reader.readAsDataURL(file);
        }
    }

    filesAry.forEach(file => size += file[1].size);

    var TotalSize = +(Math.round((size / 1024000) + "e+2") + "e-2");
    document.querySelector("#uploadContent").textContent = `已上傳的照片大小：${TotalSize} / 4MB`;
    $(".progress-bar-div").show();
    $(".progress-bar").show();
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
        document.querySelector(".progress-bar").style.width = `100%`;
        document.querySelector(".progress-bar").classList.add('bg-danger');
        document.querySelector("#submit-btn").setAttribute('disabled', 'disabled');
    }
}