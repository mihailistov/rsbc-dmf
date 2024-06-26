// import { IdentityProvider } from '../enums/identity-provider.enum';
// import { UserIdentity } from './user-identity.model';
// import { IUserResolver, User } from './user.model';

// export class IdirUser implements User {
//   public readonly identityProvider: IdentityProvider;
//   public userId: string;
//   public idpId: string;
//   public firstName: string;
//   public lastName: string;
//   public roles: string[];

//   public constructor({ accessTokenParsed, brokerProfile }: UserIdentity) {
//     const { firstName, lastName } = brokerProfile;
//     const {
//       identity_provider,
//       preferred_username: idpId,
//       sub: userId,
//       realm_access: keycloakRoles,
//     } = accessTokenParsed;

//     this.identityProvider = identity_provider;
//     this.userId = userId;
//     this.idpId = idpId;
//     this.firstName = firstName;
//     this.lastName = lastName;
//     this.roles = Object.values(keycloakRoles || {}).flatMap(
//       (clientRoles: { roles: any }) => clientRoles.roles
//     );
//   }
// }

// export class IdirResolver implements IUserResolver<IdirUser> {
//   public constructor(public userIdentity: UserIdentity) {}
//   public resolve(): IdirUser {
//     return new IdirUser(this.userIdentity);
//   }
// }
