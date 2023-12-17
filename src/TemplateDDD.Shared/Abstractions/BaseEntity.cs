using TemplateDDD.Shared.Common;

namespace TemplateDDD.Shared.Abstractions
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public interface IActiveState
    {
        bool IsActive { get; set; }
    }

    public interface IDateAudit
    {
        DateTime DateCreated { get; set; }
        DateTime? DateLastUpdated { get; set; }
    }

    public interface IDateAuditActor
    {
        string CreatedBy { get; set; }
        string? LastUpdatedBy { get; set; }
    }

    public interface IRecordArchive
    {
        bool Archived { get; set; }
        DateTime? DateArchived { get; set; }
    }

    public interface IRecordArchiveActor : IRecordArchive
    {
        string? ArchivedBy { get; set; }
    }

    public interface IAudit : IDateAudit, IDateAuditActor
    {
    }

    public interface IFullAudit : IAudit, IRecordArchiveActor
    {
    }

    public abstract class Entity : IEntity
    {
        public Entity()
        {
            Id = Utils.UniqueId();
        }

        public Guid Id { get; set; }
    }

    public interface ILifeTime
    {
        DateTime ExpiryDate { get; set; }
    }

    public interface IApproval
    {
        public string ApprovalStatus { get; set; }
        public string? ApprovalActionBy { get; set; }
        public DateTime? ApprovalActionDate { get; set; }
        public string? ApprovalActionReason { get; set; }
    }

    public abstract class Approval : IApproval
    {
        public string ApprovalStatus { get; set; } = string.Empty;
        public string? ApprovalActionBy { get; set; }
        public DateTime? ApprovalActionDate { get; set; }
        public string? ApprovalActionReason { get; set; } = string.Empty;
    }

    public abstract class AuditEntity : Entity, IAudit
    {
        public AuditEntity()
        {
            Id = Utils.UniqueId();
            DateCreated = DateTime.UtcNow;
        }

        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }

        public string CreatedBy { get; set; } = String.Empty;
        public string? LastUpdatedBy { get; set; }
    }

    public abstract class BaseEntity : AuditEntity, IFullAudit, IActiveState
    {
        public BaseEntity()
        {
            DateTime currentDate = DateTime.UtcNow;

            Id = Utils.UniqueId();
            DateCreated = currentDate;
            DateLastUpdated = currentDate;
            IsActive = true;
        }

        public bool IsActive { get; set; }
        public bool Archived { get; set; }
        public DateTime? DateArchived { get; set; }
        public string? ArchivedBy { get; set; }
    }

    public abstract class BaseApprovalEntity : AuditEntity, IFullAudit, IActiveState, IApproval
    {
        public BaseApprovalEntity()
        {
            Id = Utils.UniqueId();
            DateCreated = DateTime.UtcNow;
            IsActive = true;
        }

        public bool IsActive { get; set; }
        public bool Archived { get; set; }
        public DateTime? DateArchived { get; set; }
        public string? ArchivedBy { get; set; }

        public string ApprovalStatus { get; set; } = String.Empty;
        public string? ApprovalActionBy { get; set; }
        public DateTime? ApprovalActionDate { get; set; }
        public string? ApprovalActionReason { get; set; }
    }

    public abstract class CacheAuditEntity : IAudit
    {
        public CacheAuditEntity()
        {
            DateCreated = DateTime.UtcNow;
        }

        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }

        public string CreatedBy { get; set; } = String.Empty;
        public string? LastUpdatedBy { get; set; } = String.Empty;
    }

    public abstract class CacheBaseEntity : AuditEntity, ILifeTime
    {
        public CacheBaseEntity()
        {
            DateTime currentDate = DateTime.UtcNow;

            Id = Utils.UniqueId();
            DateCreated = currentDate;
            DateLastUpdated = currentDate;
            ExpiryDate = currentDate;
        }

        public DateTime ExpiryDate { get; set; }
    }
}