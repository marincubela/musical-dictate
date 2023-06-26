import { Api } from "..";

export default class GroupsService {
    static getGroups() {
        return Api.get({ url: '/api/studentgroups' })
    }

    static getGroup(groupId) {
        if (!groupId) return;

        return Api.get({ url: `/api/studentgroups/${groupId}` })
    }
}