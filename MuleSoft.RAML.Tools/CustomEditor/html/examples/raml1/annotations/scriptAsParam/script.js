function complete(v) {
    if (v.indexOf('a') == 0) {
        return ['atom', 'annotation', 'hello'];
    }
    if (v.indexOf('b') == 0) {
        return ['basic', 'barbara', 'begin'];
    }
}
module.exports = complete;
//# sourceMappingURL=script.js.map