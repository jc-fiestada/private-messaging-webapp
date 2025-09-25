import * as animation from "../animation/animation.js";
export function statusCode(statusCode, errors) {
    if (statusCode === 400 && errors != null) {
        errors?.forEach(error => {
            animation.showToast(error);
        });
    }
    if (statusCode === 401 && errors != null) {
        errors?.forEach(error => {
            animation.showToast(error);
        });
        console.log("trigger 401 animation");
    }
}
//# sourceMappingURL=responseHandler.js.map