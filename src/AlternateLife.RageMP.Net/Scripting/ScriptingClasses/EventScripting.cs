using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AlternateLife.RageMP.Net.Enums;
using AlternateLife.RageMP.Net.Helpers;
using AlternateLife.RageMP.Net.Interfaces;
using AlternateLife.RageMP.Net.Native;

namespace AlternateLife.RageMP.Net.Scripting.ScriptingClasses
{
    internal class EventScripting : IEventScripting
    {
        private readonly Plugin _plugin;
        private readonly RemoteEventHandler _remoteEventHandler;

        private readonly EventHandler<NativeEntityCreatedDelegate, EntityCreatedDelegate> _entityCreated;
        public event EntityCreatedDelegate EntityCreated
        {
            add => _entityCreated.Subscribe(value);
            remove => _entityCreated.Unsubscribe(value);
        }

        private readonly EventHandler<NativeEntityDestroyedDelegate, EntityDestroyedDelegate> _entityDestroyed;
        public event EntityDestroyedDelegate EntityDestroyed
        {
            add => _entityDestroyed.Subscribe(value);
            remove => _entityDestroyed.Unsubscribe(value);
        }

        private readonly EventHandler<NativeEntityModelChangeDelegate, EntityModelChangeDelegate> _entityModelChange;
        public event EntityModelChangeDelegate EntityModelChange
        {
            add => _entityModelChange.Subscribe(value);
            remove => _entityModelChange.Unsubscribe(value);
        }

        private readonly EventHandler<NativeTickDelegate, TickDelegate> _tick;
        public event TickDelegate Tick
        {
            add => _tick.Subscribe(value);
            remove => _tick.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerJoinDelegate, PlayerJoinDelegate> _playerJoin;
        public event PlayerJoinDelegate PlayerJoin
        {
            add => _playerJoin.Subscribe(value);
            remove => _playerJoin.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerReadyDelegate, PlayerReadyDelegate> _playerReady;
        public event PlayerReadyDelegate PlayerReady
        {
            add => _playerReady.Subscribe(value);
            remove => _playerReady.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerDeathDelegate, PlayerDeathDelegate> _playerDeath;
        public event PlayerDeathDelegate PlayerDeath
        {
            add => _playerDeath.Subscribe(value);
            remove => _playerDeath.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerQuitDelegate, PlayerQuitDelegate> _playerQuit;
        public event PlayerQuitDelegate PlayerQuit
        {
            add => _playerQuit.Subscribe(value);
            remove => _playerQuit.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerCommandDelegate, PlayerCommandDelegate> _playerCommand;
        public event PlayerCommandDelegate PlayerCommand
        {
            add => _playerCommand.Subscribe(value);
            remove => _playerCommand.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerChatDelegate, PlayerChatDelegate> _playerChat;
        public event PlayerChatDelegate PlayerChat
        {
            add => _playerChat.Subscribe(value);
            remove => _playerChat.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerSpawnDelegate, PlayerSpawnDelegate> _playerSpawn;
        public event PlayerSpawnDelegate PlayerSpawn
        {
            add => _playerSpawn.Subscribe(value);
            remove => _playerSpawn.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerDamageDelegate, PlayerDamageDelegate> _playerDamage;
        public event PlayerDamageDelegate PlayerDamage
        {
            add => _playerDamage.Subscribe(value);
            remove => _playerDamage.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerWeaponChangeDelegate, PlayerWeaponChangeDelegate> _playerWeaponChange;
        public event PlayerWeaponChangeDelegate PlayerWeaponChange
        {
            add => _playerWeaponChange.Subscribe(value);
            remove => _playerWeaponChange.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerStartEnterVehicleDelegate, PlayerStartEnterVehicleDelegate> _playerStartEnterVehicle;
        public event PlayerStartEnterVehicleDelegate PlayerStartEnterVehicle
        {
            add => _playerStartEnterVehicle.Subscribe(value);
            remove => _playerStartEnterVehicle.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerEnterVehicleDelegate, PlayerEnterVehicleDelegate> _playerEnterVehicle;
        public event PlayerEnterVehicleDelegate PlayerEnterVehicle
        {
            add => _playerEnterVehicle.Subscribe(value);
            remove => _playerEnterVehicle.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerStartExitVehicleDelegate, PlayerStartExitVehicleDelegate> _playerStartExitVehicle;
        public event PlayerStartExitVehicleDelegate PlayerStartExitVehicle
        {
            add => _playerStartExitVehicle.Subscribe(value);
            remove => _playerStartExitVehicle.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerExitVehicleDelegate, PlayerExitVehicleDelegate> _playerExitVehicle;
        public event PlayerExitVehicleDelegate PlayerExitVehicle
        {
            add => _playerExitVehicle.Subscribe(value);
            remove => _playerExitVehicle.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerEnterCheckpointDelegate, PlayerEnterCheckpointDelegate> _playerEnterCheckpoint;
        public event PlayerEnterCheckpointDelegate PlayerEnterCheckpoint
        {
            add => _playerEnterCheckpoint.Subscribe(value);
            remove => _playerEnterCheckpoint.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerExitCheckpointDelegate, PlayerExitCheckpointDelegate> _playerExitCheckpoint;
        public event PlayerExitCheckpointDelegate PlayerExitCheckpoint
        {
            add => _playerExitCheckpoint.Subscribe(value);
            remove => _playerExitCheckpoint.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerEnterColshapeDelegate, PlayerEnterColshapeDelegate> _playerEnterColshape;
        public event PlayerEnterColshapeDelegate PlayerEnterColshape
        {
            add => _playerEnterColshape.Subscribe(value);
            remove => _playerEnterColshape.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerExitColshapeDelegate, PlayerExitColshapeDelegate> _playerExitColshape;
        public event PlayerExitColshapeDelegate PlayerExitColshape
        {
            add => _playerExitColshape.Subscribe(value);
            remove => _playerExitColshape.Unsubscribe(value);
        }

        private readonly EventHandler<NativeVehicleDeathDelegate, VehicleDeathDelegate> _vehicleDeath;
        public event VehicleDeathDelegate VehicleDeath
        {
            add => _vehicleDeath.Subscribe(value);
            remove => _vehicleDeath.Unsubscribe(value);
        }

        private readonly EventHandler<NativeVehicleSirenToggleDelegate, VehicleSirenToggleDelegate> _vehicleSirenToggle;
        public event VehicleSirenToggleDelegate VehicleSirenToggle
        {
            add => _vehicleSirenToggle.Subscribe(value);
            remove => _vehicleSirenToggle.Unsubscribe(value);
        }

        private readonly EventHandler<NativeVehicleHornToggleDelegate, VehicleHornToggleDelegate> _vehicleHornToggle;
        public event VehicleHornToggleDelegate VehicleHornToggle
        {
            add => _vehicleHornToggle.Subscribe(value);
            remove => _vehicleHornToggle.Unsubscribe(value);
        }

        private readonly EventHandler<NativeVehicleTrailerAttachedDelegate, VehicleTrailerAttachedDelegate> _vehicleTrailerAttached;
        public event VehicleTrailerAttachedDelegate VehicleTrailerAttached
        {
            add => _vehicleTrailerAttached.Subscribe(value);
            remove => _vehicleTrailerAttached.Unsubscribe(value);
        }

        private readonly EventHandler<NativeVehicleDamageDelegate, VehicleDamageDelegate> _vehicleDamage;
        public event VehicleDamageDelegate VehicleDamage
        {
            add => _vehicleDamage.Subscribe(value);
            remove => _vehicleDamage.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerCreateWaypointDelegate, PlayerCreateWaypointDelegate> _playerCreateWaypoint;
        public event PlayerCreateWaypointDelegate PlayerCreateWaypoint
        {
            add => _playerCreateWaypoint.Subscribe(value);
            remove => _playerCreateWaypoint.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerReachWaypointDelegate, PlayerReachWaypointDelegate> _playerReachWaypoint;
        public event PlayerReachWaypointDelegate PlayerReachWaypoint
        {
            add => _playerReachWaypoint.Subscribe(value);
            remove => _playerReachWaypoint.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerStreamInDelegate, PlayerStreamInDelegate> _playerStreamIn;
        public event PlayerStreamInDelegate PlayerStreamIn
        {
            add => _playerStreamIn.Subscribe(value);
            remove => _playerStreamIn.Unsubscribe(value);
        }

        private readonly EventHandler<NativePlayerStreamOutDelegate, PlayerStreamOutDelegate> _playerStreamOut;
        public event PlayerStreamOutDelegate PlayerStreamOut
        {
            add => _playerStreamOut.Subscribe(value);
            remove => _playerStreamOut.Unsubscribe(value);
        }

        internal EventScripting(Plugin plugin)
        {
            _plugin = plugin;
            _remoteEventHandler = new RemoteEventHandler(plugin);

            _tick = new EventHandler<NativeTickDelegate, TickDelegate>(plugin, EventType.Tick, DispatchTick, true);

            _entityCreated = new EventHandler<NativeEntityCreatedDelegate, EntityCreatedDelegate>(plugin, EventType.EntityCreated, DispatchEntityCreated, true);
            _entityDestroyed = new EventHandler<NativeEntityDestroyedDelegate, EntityDestroyedDelegate>(plugin, EventType.EntityDestroyed, DispatchEntityDestroyed, true);
            _entityModelChange = new EventHandler<NativeEntityModelChangeDelegate, EntityModelChangeDelegate>(plugin, EventType.EntityModelChanged, DispatchEntityModelChange);

            _playerJoin = new EventHandler<NativePlayerJoinDelegate, PlayerJoinDelegate>(plugin, EventType.PlayerJoin, DispatchPlayerJoin);
            _playerReady = new EventHandler<NativePlayerReadyDelegate, PlayerReadyDelegate>(plugin, EventType.PlayerReady, DispatchPlayerReady);
            _playerDeath = new EventHandler<NativePlayerDeathDelegate, PlayerDeathDelegate>(plugin, EventType.PlayerDeath, DispatchPlayerDeath);
            _playerQuit = new EventHandler<NativePlayerQuitDelegate, PlayerQuitDelegate>(plugin, EventType.PlayerQuit, DisaptchPlayerQuit);
            _playerCommand = new EventHandler<NativePlayerCommandDelegate, PlayerCommandDelegate>(plugin, EventType.PlayerCommand, DispatchPlayerCommand, true);
            _playerChat = new EventHandler<NativePlayerChatDelegate, PlayerChatDelegate>(plugin, EventType.PlayerChat, DispatchPlayerChat);
            _playerSpawn = new EventHandler<NativePlayerSpawnDelegate, PlayerSpawnDelegate>(plugin, EventType.PlayerSpawn, DispatchPlayerSpawn);
            _playerDamage = new EventHandler<NativePlayerDamageDelegate, PlayerDamageDelegate>(plugin, EventType.PlayerDamage, DispatchPlayerDamage);
            _playerWeaponChange = new EventHandler<NativePlayerWeaponChangeDelegate, PlayerWeaponChangeDelegate>(plugin, EventType.PlayerWeaponChange, DispatchPlayerWeaponChange);

            _playerStartEnterVehicle = new EventHandler<NativePlayerStartEnterVehicleDelegate, PlayerStartEnterVehicleDelegate>(plugin, EventType.PlayerStartEnterVehicle, DispatchStartEnterVehicle);
            _playerEnterVehicle = new EventHandler<NativePlayerEnterVehicleDelegate, PlayerEnterVehicleDelegate>(plugin, EventType.PlayerEnterVehicle, DispatchPlayerEnterVehicle);
            _playerStartExitVehicle = new EventHandler<NativePlayerStartExitVehicleDelegate, PlayerStartExitVehicleDelegate>(plugin, EventType.PlayerStartExitVehicle, DispatchStartExitVehicle);
            _playerExitVehicle = new EventHandler<NativePlayerExitVehicleDelegate, PlayerExitVehicleDelegate>(plugin, EventType.PlayerExitVehicle, DispatchPlayerExitVehicle);

            _playerEnterCheckpoint = new EventHandler<NativePlayerEnterCheckpointDelegate, PlayerEnterCheckpointDelegate>(plugin, EventType.PlayerEnterCheckpoint, DispatchPlayerEnterCheckpoint);
            _playerExitCheckpoint = new EventHandler<NativePlayerExitCheckpointDelegate, PlayerExitCheckpointDelegate>(plugin, EventType.PlayerExitCheckpoint, DispatchPlayerExitCheckpoint);

            _playerEnterColshape = new EventHandler<NativePlayerEnterColshapeDelegate, PlayerEnterColshapeDelegate>(plugin, EventType.PlayerEnterColshape, DispatchPlayerEnterColshape);
            _playerExitColshape = new EventHandler<NativePlayerExitColshapeDelegate, PlayerExitColshapeDelegate>(plugin, EventType.PlayerExitColshape, DispatchPlayerExitColshape);

            _playerCreateWaypoint = new EventHandler<NativePlayerCreateWaypointDelegate, PlayerCreateWaypointDelegate>(plugin, EventType.PlayerCreateWaypoint, DispatchPlayerCreateWaypoint);
            _playerReachWaypoint = new EventHandler<NativePlayerReachWaypointDelegate, PlayerReachWaypointDelegate>(plugin, EventType.PlayerReachWaypoint, DispatchPlayerReachWaypoint);

            _playerStreamIn = new EventHandler<NativePlayerStreamInDelegate, PlayerStreamInDelegate>(plugin, EventType.PlayerStreamIn, DispatchPlayerStreamIn);
            _playerStreamOut = new EventHandler<NativePlayerStreamOutDelegate, PlayerStreamOutDelegate>(plugin, EventType.PlayerStreamOut, DispatchPlayerStreamOut);

            _vehicleDeath = new EventHandler<NativeVehicleDeathDelegate, VehicleDeathDelegate>(plugin, EventType.VehicleDeath, DispatchVehicleDeath);
            _vehicleSirenToggle = new EventHandler<NativeVehicleSirenToggleDelegate, VehicleSirenToggleDelegate>(plugin, EventType.VehicleSirenToggle, DispatchVehicleSirenToggle);
            _vehicleHornToggle = new EventHandler<NativeVehicleHornToggleDelegate, VehicleHornToggleDelegate>(plugin, EventType.VehicleHornToggle, DispatchVehicleHornToggle);
            _vehicleTrailerAttached = new EventHandler<NativeVehicleTrailerAttachedDelegate, VehicleTrailerAttachedDelegate>(plugin, EventType.VehicleTrailerAttached, DispatchTrailerAttached);
            _vehicleDamage = new EventHandler<NativeVehicleDamageDelegate, VehicleDamageDelegate>(plugin, EventType.VehicleDamage, DispatchVehicleDamage);
        }

        public void Add(string eventName, PlayerRemoteEventDelegate callback)
        {
            Contract.NotEmpty(eventName, nameof(eventName));

            _remoteEventHandler.Subscribe(eventName, callback);
        }

        private void DispatchEntityCreated(IntPtr entitypointer)
        {
            if (TryBuildEntity(entitypointer, out IEntity createdEntity) == false)
            {
                return;
            }

            _entityCreated.CallAsync(x => x(createdEntity));
        }

        private void DispatchEntityDestroyed(IntPtr entitypointer)
        {
            TryRemoveEntity(entitypointer, entity =>
            {
                _entityDestroyed.CallAsync(x => x(entity));
            });
        }

        private void DispatchEntityModelChange(IntPtr entitypointer, uint oldmodel)
        {
            if (TryGetEntity(entitypointer, out IEntity entity) == false)
            {
                return;
            }

            _entityModelChange.CallAsync(x => x(entity, oldmodel));
        }

        private void DispatchTick()
        {
            _tick.CallAsync(x => x());

            _plugin.TickScheduler();
        }

        private void DispatchPlayerJoin(IntPtr playerPointer)
        {
            var player = _plugin.PlayerPool[playerPointer];

            _playerJoin.CallAsync(x => x(player));
        }

        private void DispatchPlayerReady(IntPtr playerPointer)
        {
            var player = _plugin.PlayerPool[playerPointer];

            _playerReady.CallAsync(x => x(player));
        }

        private void DispatchPlayerDeath(IntPtr playerPointer, uint reason, IntPtr killerplayerpointer)
        {
            var player = _plugin.PlayerPool[playerPointer];
            var killer = _plugin.PlayerPool[killerplayerpointer];

            _playerDeath.CallAsync(x => x(player, reason, killer));
        }

        private void DisaptchPlayerQuit(IntPtr playerPointer, uint type, IntPtr reason)
        {
            var player = _plugin.PlayerPool[playerPointer];
            var message = StringConverter.PointerToString(reason);

            _playerQuit.Call(x => x(player, (DisconnectReason)type, message));
        }

        private async void DispatchPlayerCommand(IntPtr playerPointer, IntPtr text)
        {
            var player = _plugin.PlayerPool[playerPointer];
            var message = Marshal.PtrToStringUni(text);

            var eventArgs = new CommandEventArgs();

            await _playerCommand
                .CallAsyncAwaitable(x => x(player, message, eventArgs))
                .ConfigureAwait(false);

            if (eventArgs.Cancelled)
            {
                return;
            }

            await Task.Run(() => _plugin.Commands.ExecuteCommand(player, message))
                .ConfigureAwait(false);
        }

        private void DispatchPlayerChat(IntPtr playerPointer, IntPtr text)
        {
            var player = _plugin.PlayerPool[playerPointer];
            var message = Marshal.PtrToStringUni(text);

            _playerChat.CallAsync(x => x(player, message));
        }

        private void DispatchPlayerSpawn(IntPtr playerPointer)
        {
            var player = _plugin.PlayerPool[playerPointer];

            _playerSpawn.CallAsync(x => x(player));
        }

        private void DispatchPlayerDamage(IntPtr playerPointer, float healthLoss, float armorLoss)
        {
            var player = _plugin.PlayerPool[playerPointer];

            _playerDamage.CallAsync(x => x(player, healthLoss, armorLoss));
        }

        private void DispatchPlayerWeaponChange(IntPtr playerPointer, uint oldWeapon, uint newWeapon)
        {
            var player = _plugin.PlayerPool[playerPointer];

            _playerWeaponChange.CallAsync(x => x(player, oldWeapon, newWeapon));
        }

        private void DispatchStartEnterVehicle(IntPtr playerPointer, IntPtr vehiclePointer, int seat)
        {
            var player = _plugin.PlayerPool[playerPointer];
            var vehicle = _plugin.VehiclePool[vehiclePointer];

            _playerStartEnterVehicle.CallAsync(x => x(player, vehicle, seat));
        }

        private void DispatchPlayerEnterVehicle(IntPtr playerPointer, IntPtr vehiclePointer, int seat)
        {
            var player = _plugin.PlayerPool[playerPointer];
            var vehicle = _plugin.VehiclePool[vehiclePointer];

            _playerEnterVehicle.CallAsync(x => x(player, vehicle, seat));
        }

        private void DispatchStartExitVehicle(IntPtr playerPointer, IntPtr vehiclePointer)
        {
            var player = _plugin.PlayerPool[playerPointer];
            var vehicle = _plugin.VehiclePool[vehiclePointer];

            _playerStartExitVehicle.CallAsync(x => x(player, vehicle));
        }

        private void DispatchPlayerExitVehicle(IntPtr playerPointer, IntPtr vehiclePointer)
        {
            var player = _plugin.PlayerPool[playerPointer];
            var vehicle = _plugin.VehiclePool[vehiclePointer];

            _playerExitVehicle.CallAsync(x => x(player, vehicle));
        }

        private void DispatchPlayerEnterCheckpoint(IntPtr playerpointer, IntPtr checkpointPointer)
        {
            var player = _plugin.PlayerPool[playerpointer];
            var checkpoint = _plugin.CheckpointPool[checkpointPointer];

            _playerEnterCheckpoint.CallAsync(x => x(player, checkpoint));
        }

        private void DispatchPlayerExitCheckpoint(IntPtr playerpointer, IntPtr checkpointPointer)
        {
            var player = _plugin.PlayerPool[playerpointer];
            var checkpoint = _plugin.CheckpointPool[checkpointPointer];

            _playerExitCheckpoint.CallAsync(x => x(player, checkpoint));
        }

        private void DispatchPlayerEnterColshape(IntPtr playerpointer, IntPtr checkpointPointer)
        {
            var player = _plugin.PlayerPool[playerpointer];
            var checkpoint = _plugin.ColshapePool[checkpointPointer];

            _playerEnterColshape.CallAsync(x => x(player, checkpoint));
        }

        private void DispatchPlayerExitColshape(IntPtr playerpointer, IntPtr checkpointPointer)
        {
            var player = _plugin.PlayerPool[playerpointer];
            var checkpoint = _plugin.ColshapePool[checkpointPointer];

            _playerExitColshape.CallAsync(x => x(player, checkpoint));
        }

        private void DispatchVehicleDeath(IntPtr vehiclepointer, uint reason, IntPtr killerplayerpointer)
        {
            var vehicle = _plugin.VehiclePool[vehiclepointer];
            var killerPlayer = _plugin.PlayerPool[killerplayerpointer];

            _vehicleDeath.CallAsync(x => x(vehicle, reason, killerPlayer));
        }

        private void DispatchVehicleSirenToggle(IntPtr vehiclepointer, bool toggle)
        {
            var vehicle = _plugin.VehiclePool[vehiclepointer];

            _vehicleSirenToggle.CallAsync(x => x(vehicle, toggle));
        }

        private void DispatchVehicleHornToggle(IntPtr vehiclepointer, bool toggle)
        {
            var vehicle = _plugin.VehiclePool[vehiclepointer];

            _vehicleSirenToggle.CallAsync(x => x(vehicle, toggle));
        }

        private void DispatchTrailerAttached(IntPtr vehiclepointer, IntPtr trailerpointer)
        {
            var vehicle = _plugin.VehiclePool[vehiclepointer];
            var trailer = _plugin.VehiclePool[trailerpointer];

            _vehicleTrailerAttached.CallAsync(x => x(vehicle, trailer));
        }

        private void DispatchVehicleDamage(IntPtr vehiclepointer, float bodyhealthloss, float enginehealthloss)
        {
            var vehicle = _plugin.VehiclePool[vehiclepointer];

            _vehicleDamage.CallAsync(x => x(vehicle, bodyhealthloss, enginehealthloss));
        }

        private void DispatchPlayerCreateWaypoint(IntPtr playerpointer, Vector3 position)
        {
            var player = _plugin.PlayerPool[playerpointer];

            _playerCreateWaypoint.CallAsync(x => x(player, position));
        }

        private void DispatchPlayerReachWaypoint(IntPtr playerpointer)
        {
            var player = _plugin.PlayerPool[playerpointer];

            _playerReachWaypoint.CallAsync(x => x(player));
        }

        private void DispatchPlayerStreamIn(IntPtr playerpointer, IntPtr forplayerpointer)
        {
            var player = _plugin.PlayerPool[playerpointer];
            var forPlayer = _plugin.PlayerPool[forplayerpointer];

            _playerStreamIn.CallAsync(x => x(player, forPlayer));
        }

        private void DispatchPlayerStreamOut(IntPtr playerpointer, IntPtr forplayerpointer)
        {
            var player = _plugin.PlayerPool[playerpointer];
            var forPlayer = _plugin.PlayerPool[forplayerpointer];

            _playerStreamOut.CallAsync(x => x(player, forPlayer));
        }

        private bool GetPoolFromPointer(IntPtr entityPointer, out IInternalPool pool, out EntityType type)
        {
            type = (EntityType) Rage.Entity.Entity_GetType(entityPointer);

            if (_plugin.TryGetPool(type, out pool) == false)
            {
                pool = null;

                return false;
            }

            return true;
        }

        private bool TryBuildEntity(IntPtr entityPointer, out IEntity createdEntity)
        {
            if (GetPoolFromPointer(entityPointer, out IInternalPool pool, out _) == false)
            {
                createdEntity = null;

                return false;
            }

            return pool.CreateAndSaveEntity(entityPointer, out createdEntity);
        }

        private bool TryGetEntity(IntPtr entityPointer, out IEntity entity)
        {
            if (GetPoolFromPointer(entityPointer, out IInternalPool pool, out _) == false)
            {
                entity = null;

                return false;
            }

            entity = pool.GetEntity(entityPointer);

            return entity != null;
        }

        private void TryRemoveEntity(IntPtr entityPointer, Action<IEntity> preRemoveCallback)
        {
            if (GetPoolFromPointer(entityPointer, out IInternalPool pool, out _) == false)
            {
                return;
            }

            pool.RemoveEntity(entityPointer, preRemoveCallback);
        }
    }
}
